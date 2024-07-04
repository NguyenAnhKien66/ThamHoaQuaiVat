using System.Collections;
using UnityEngine;

public class QuanLyQuai : MonoBehaviour
{
    private NhanVat nhanVat;
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    public int LuongMau = 100;
    public bool AnimationTanCong;
    public int KinhNghiemNhanDuoc;
    public QuanLyVatPham quanLyVatPham;
    public int MauToiDa = 100;
    public int MauHienTai;
    public float ThoiGianTangMau = 60f;

    private void Start()
    {
        // Lượng máu của quái được thiết lập tại thời điểm sinh ra
        MauHienTai = LuongMau;
        Debug.Log("Bắt đầu với lượng máu: " + MauHienTai);

        StartCoroutine(TangMauTheoThoiGian());
    }

    IEnumerator TangMauTheoThoiGian()
    {
        while (true)
        {
            yield return new WaitForSeconds(ThoiGianTangMau);
            Debug.Log("Kiểm tra tăng máu, MauHienTai: " + MauHienTai + ", MauToiDa: " + MauToiDa);

            if (MauHienTai < MauToiDa)
            {
                MauHienTai += 20;
                if (MauHienTai > MauToiDa)
                {
                    MauHienTai = MauToiDa;
                }
                LuongMau = MauHienTai;
                Debug.Log("Đã tăng lượng máu của quái lên " + MauHienTai);
            }
        }
    }

    void Awake()
    {
        if (quanLyVatPham == null)
        {
            quanLyVatPham = FindObjectOfType<QuanLyVatPham>();
        }

        if (quanLyVatPham == null)
        {
            Debug.LogError("QuanLyVatPham không tìm thấy");
        }
    }

    public void SatThuongQuaiGanhChieu(int SatThuong, NhanVat nhanVat)
    {
        this.nhanVat = nhanVat;

        if (LuongMau > 0)
        {
            LuongMau -= SatThuong;
            Debug.Log("Quái nhận sát thương " + SatThuong + ". Lượng máu còn lại: " + LuongMau);
        }

        if (LuongMau <= 0)
        {
            if (this.nhanVat != null)
            {
                this.nhanVat.CapNhatKinhNghiem(KinhNghiemNhanDuoc);
                DemQuaiChet.instance.ThemSoluong();

                if (quanLyVatPham != null)
                {
                    quanLyVatPham.RoiVatPham(transform.position);
                }
                else
                {
                    Debug.LogError("quanLyVatPham is null");
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GetComponent<Animator>() != null && AnimationTanCong)
            {
                GetComponent<Animator>().SetBool("VaCham", true);
            }

            nhanVat = collision.GetComponent<NhanVat>();
            if (nhanVat != null)
            {
                InvokeRepeating("SatThuongQuaigayRa", 0, 0.1f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GetComponent<Animator>() != null && AnimationTanCong)
            {
                GetComponent<Animator>().SetBool("VaCham", false);
            }

            CancelInvoke("SatThuongQuaigayRa");
        }
    }

    void SatThuongQuaigayRa()
    {
        if (nhanVat != null)
        {
            int SatThuong = Random.Range(SatThuongNhoNhat, SatThuongLonNhat);
            Debug.Log("Player nhận sát thương " + SatThuong);
            nhanVat.SatThuongGanhChieu(SatThuong);
        }
    }
}
