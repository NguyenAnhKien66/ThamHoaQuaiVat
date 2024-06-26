using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhanVat : MonoBehaviour
{
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    private int kinhNghiemNhanVat;
    public SpriteRenderer QuanLyNhanVat; // Khai bao tham chieu den mot thanh phan SpriteRenderer
    private Rigidbody2D rb;
    public Vector3 HuongDiChuyen; // Khai bao huong di chuyen nhan vat
    public float HoiChieuCuon = 10f; // Thoi gian cho giua cac lan cuon
    private float ThoiGianHoiChieu = 0f; // Dem thoi gian con lai cho cooldown
    private bool DangCuon = false; // Bien de kiem tra xem dang cuon hay khong
    public Animator animator; // Khai bao tham chieu den thanh phan Animator

    // Tham chieu den thanh ky nang
    public ThanhKyNangNhanVat thanhKyNangNhanVat;

    private void Start()
    {
        kinhNghiemNhanVat = 0;
    }
    public void CapNhatKinhNghiem(int kinhNghiem)
    {
        kinhNghiemNhanVat += kinhNghiem;
        if (kinhNghiemNhanVat >= quanLyThongSoNhanVat.KinhNghiemToiDa)
        {
            kinhNghiemNhanVat = 0; // Reset Kinh nghiem sau khi moi lan len cap
            CapNhatCapDo();
        }
    }
    private void CapNhatCapDo()
    {
        quanLyThongSoNhanVat.CapDoNhanVat++;
        quanLyThongSoNhanVat.SatThuongLonNhat += quanLyThongSoNhanVat.SatThuongCongThemKhiThangCap;
        quanLyThongSoNhanVat.SatThuongNhoNhat += quanLyThongSoNhanVat.SatThuongCongThemKhiThangCap;
        Debug.Log("Level: " + quanLyThongSoNhanVat.CapDoNhanVat);
        // Them Cac thong so khac
    }
    private void Update()
    {
        // Lay du lieu tu ban phim
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        HuongDiChuyen = new Vector3(horizontalInput, verticalInput, 0);

        // Cap nhat vi tri nhan vat
        transform.position += HuongDiChuyen * quanLyThongSoNhanVat.TocDoNhanVat * Time.deltaTime;


        // Cap nhat bien TocDo trong Animator
        QuanLyNhanVat.GetComponent<Animator>().SetFloat("TocDo", HuongDiChuyen.sqrMagnitude);

        // Kiem tra neu khong DangCuon va cooldown da het
        if (!DangCuon && ThoiGianHoiChieu <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Bat dau cuon
                QuanLyNhanVat.GetComponent<Animator>().SetBool("Cuon", true);
                quanLyThongSoNhanVat.TocDoNhanVat += quanLyThongSoNhanVat.TocDoCuonNhanVat; // Tang toc do di chuyen
                ThoiGianHoiChieu = HoiChieuCuon; // Reset cooldown
                DangCuon = true; // Cap nhat trang thai dang cuon
                StartCoroutine(StopRollingAfterTime(quanLyThongSoNhanVat.ThoiGianCuonNhanVat)); // Goi coroutine de dung cuon sau thoi gian dinh truoc
            }
        }

        // Giam cooldown neu no lon hon 0
        if (ThoiGianHoiChieu > 0)
        {
            ThoiGianHoiChieu -= Time.deltaTime;
            // Cap nhat thanh ky nang
            thanhKyNangNhanVat.CapNhatHoiChieu(1 - ThoiGianHoiChieu / HoiChieuCuon);
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
        quanLyThongSoNhanVat.TocDoNhanVat -= quanLyThongSoNhanVat.ThoiGianCuonNhanVat;

        // Cap nhat trang thai khong con cuon nua
        DangCuon = false;
    }

    public ThanhMauNhanVat thanhMauNhanVat;
    public void SatThuongGanhChieu(int SatThuong)
    {
        thanhMauNhanVat.NhanSatThuong(SatThuong);
    }
}
