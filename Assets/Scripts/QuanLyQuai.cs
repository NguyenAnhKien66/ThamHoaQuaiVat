using System.Collections;
using System.Collections.Generic;
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
        MauHienTai = LuongMau; 
        Debug.Log("Starting with health: " + MauHienTai);
        StartCoroutine(TangMauTheoThoiGian()); 
    }

    IEnumerator TangMauTheoThoiGian()
    {
        while (true)
        {
            yield return new WaitForSeconds(ThoiGianTangMau);
            Debug.Log("Checking health regeneration, MauHienTai: " + MauHienTai + ", MauToiDa: " + MauToiDa);

            if (MauHienTai < MauToiDa)
            {
                MauHienTai += 20;
                if (MauHienTai > MauToiDa)
                {
                    MauHienTai = MauToiDa;
                }
                LuongMau = MauHienTai;
                Debug.Log("Increased monster health to " + MauHienTai);
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
            Debug.LogError("QuanLyVatPham not found");
        }
    }

    public void SatThuongQuaiGanhChieu(int SatThuong, NhanVat nhanVat)
    {
        this.nhanVat = nhanVat;

        if (LuongMau > 0)
        {
            LuongMau -= SatThuong;
            Debug.Log("Monster took damage " + SatThuong + ". Remaining health: " + LuongMau);
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

                FindObjectOfType<QuanLyPhatSinhQuai>().demquai();
                /*ReturnToPool(); */
                Destroy(gameObject);
            }
        }
    }

    void ReturnToPool()
    {
        gameObject.SetActive(false); 
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
            Debug.Log("Player took damage " + SatThuong);
            nhanVat.SatThuongGanhChieu(SatThuong);
            
        }
    }
}
