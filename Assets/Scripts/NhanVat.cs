using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhanVat : MonoBehaviour
{
    public SpriteRenderer QuanLyNhanVat; // Khai bao tham chieu den mot thanh phan SpriteRenderer
    public float TocDoDiChuyen = 5f; // Khai bao toc do di chuyen mac dinh
    public float TocDoCuon = 2f; // Khai bao toc do cuon
    public float ThoiGianCuon; // Khai bao thoi gian cuon
    private Rigidbody2D rb;
    public Vector3 HuongDiChuyen; // Khai bao huong di chuyen nhan vat
    public float HoiChieuCuon = 10f; // Thoi gian cho giua cac lan cuon
    private float ThoiGianHoiChieu = 0f; // Dem thoi gian con lai cho cooldown
    private bool DangCuon = false; // Bien de kiem tra xem dang cuon hay khong
    public Animator animator; // Khai bao tham chieu den thanh phan Animator

    private void Update()
    {
        // Lay du lieu tu ban phim
        HuongDiChuyen.x = Input.GetAxis("Horizontal");
        HuongDiChuyen.y = Input.GetAxis("Vertical");

        // Cap nhat vi tri nhan vat
        transform.position += HuongDiChuyen * TocDoDiChuyen * Time.deltaTime;

        // Cap nhat bien TocDo trong Animator
        QuanLyNhanVat.GetComponent<Animator>().SetFloat("TocDo", HuongDiChuyen.sqrMagnitude);

        // Kiem tra neu khong DangCuon va cooldown da het
        if (!DangCuon && ThoiGianHoiChieu <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Bat dau cuon
                QuanLyNhanVat.GetComponent<Animator>().SetBool("Cuon", true);
                TocDoDiChuyen += TocDoCuon; // Tang toc do di chuyen
                ThoiGianHoiChieu = HoiChieuCuon; // Reset cooldown
                DangCuon = true; // Cap nhat trang thai dang cuon
                StartCoroutine(StopRollingAfterTime(ThoiGianCuon)); // Goi coroutine de dung cuon sau thoi gian dinh truoc
            }
        }

        // Giam cooldown neu no lon hon 0
        if (ThoiGianHoiChieu > 0)
        {
            ThoiGianHoiChieu -= Time.deltaTime;
        }

        // Xu ly huong cua nhan vat
        if (HuongDiChuyen.x != 0)
        {
            if (HuongDiChuyen.x > 0)
            {
                // Quay nhan vat ve ben phai
                QuanLyNhanVat.transform.localScale = new Vector3(1f, 1f, 0);
            }
            else
            {
                // Quay nhan vat ve ben trai
                QuanLyNhanVat.transform.localScale = new Vector3(-1f, 1f, 0);
            }
        }
    }

    // Khai bao mot IEnumerator co ten la StopRollingAfterTime, nhan vao mot tham so float time
    IEnumerator StopRollingAfterTime(float time)
    {
        // Tam dung coroutine trong khoang thoi gian duoc chi dinh (time) tinh bang giay
        yield return new WaitForSeconds(time);

        // Dat bien Cuon trong Animator ve false
        QuanLyNhanVat.GetComponent<Animator>().SetBool("Cuon", false);

        // Giam toc do di chuyen ve gia tri cu
        TocDoDiChuyen -= ThoiGianCuon;

        // Cap nhat trang thai khong con cuon nua
        DangCuon = false;
    }
    public ThanhMauNhanVat thanhMauNhanVat;
    public void SatThuongGanhChieu(int SatThuong)
    {
        thanhMauNhanVat.NhanSatThuong(SatThuong);
    }
}
