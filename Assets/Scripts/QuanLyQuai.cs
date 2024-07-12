using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyQuai : MonoBehaviour
{
    private NhanVat nhanVat; // Player character
    public int SatThuongNhoNhat; // Minimum damage
    public int SatThuongLonNhat; // Maximum damage
    public int LuongMau = 100; // Health
    public bool AnimationTanCong; // Attack animation flag
    public int KinhNghiemNhanDuoc; // Experience points gained upon defeat
    public QuanLyVatPham quanLyVatPham; // Item manager
    public int MauToiDa = 100; // Maximum health
    public int MauHienTai; // Current health
    public float ThoiGianTangMau = 60f; // Health regeneration interval

    private void Start()
    {
        MauHienTai = LuongMau; // Set initial health
        Debug.Log("Starting with health: " + MauHienTai);
        StartCoroutine(TangMauTheoThoiGian()); // Start health regeneration
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
                /*ReturnToPool(); */// Return to pool upon death
                Destroy(gameObject);
            }
        }
    }

    void ReturnToPool()
    {
        gameObject.SetActive(false); // Disable for reuse
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
