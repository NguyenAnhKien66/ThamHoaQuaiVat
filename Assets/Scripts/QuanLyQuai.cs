using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyQuai : MonoBehaviour
{
    private NhanVat nhanVat;
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    public int LuongMau;
    public bool AnimationTanCong;

    GameObject obj;
    public QuanLyVatPham quanLyVatPham;

    void Awake()
    {
        LuongMau = 100;

        // Tìm đối tượng QuanLyVatPham nếu chưa được gán
        if (quanLyVatPham == null)
        {
            quanLyVatPham = FindObjectOfType<QuanLyVatPham>();
        }

        if (quanLyVatPham == null)
        {
            Debug.LogError("QuanLyVatPham not found in the scene. Please ensure it is added and active.");
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
                this.nhanVat.CapNhatKinhNghiem(10);
                DemQuaiChet.instance.ThemSoluong(); // Đảm bảo gọi đúng thứ tự cập nhật

                // Kiểm tra quanLyVatPham không null trước khi gọi phương thức
                if (quanLyVatPham != null)
                {
                    quanLyVatPham.RoiVatPham(transform.position); // Gọi phương thức rơi vật phẩm
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
            // animation quái đánh
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
            // Ngừng animation quái đánh
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
            int SatThuong = UnityEngine.Random.Range(SatThuongNhoNhat, SatThuongLonNhat);
            Debug.Log("Player nhận sát thương " + SatThuong);
            nhanVat.SatThuongGanhChieu(SatThuong);
        }
    }
}
