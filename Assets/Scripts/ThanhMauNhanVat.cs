using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ThanhMauNhanVat : MonoBehaviour
{
    [SerializeField] QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    public ThanhMau thanhMau;
    public UnityEvent SauKhiMatMang;

    private void Start()
    {
        quanLyThongSoNhanVat.MauHientai = quanLyThongSoNhanVat.MauToiDaNhanVat;
        thanhMau.CapnhatMau(quanLyThongSoNhanVat.MauHientai, quanLyThongSoNhanVat.MauToiDaNhanVat);
    }

    private void OnEnable()
    {
        SauKhiMatMang.AddListener(Matmang);
    }

    private void OnDisable()
    {
        SauKhiMatMang.RemoveListener(Matmang);
    }

    public void NhanSatThuong(int SatThuong)
    {
        quanLyThongSoNhanVat.MauHientai -= SatThuong;

        Debug.Log("Có trừ máu");
        if (quanLyThongSoNhanVat.MauHientai <= 0)
        {
            quanLyThongSoNhanVat.MauHientai = 0;
            SauKhiMatMang.Invoke();
        }
        thanhMau.CapnhatMau(quanLyThongSoNhanVat.MauHientai, quanLyThongSoNhanVat.MauToiDaNhanVat);
    }

    public void Matmang()
    {
        // Tạo và cập nhật dữ liệu game
        DuLieuGame gameData = DieuKhienGame.instance.duLieuGame;
        if (gameData != null && gameData.maps.Length > 0)
        {
            // Lưu số lượng kills
            if (DemQuaiChet.instance != null)
            {
                gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai = DemQuaiChet.instance.LaySoLuongHienTai();
                if(gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai >= gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillCaoNhat)
                {
                    gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillCaoNhat = gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai;
                }
            }

            // Lưu thời gian trôi qua
            DongHo dongHo = FindObjectOfType<DongHo>();
            if (dongHo != null)
            {
                gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai = dongHo.GetThoiGianTroiQua();
                if(gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai >= gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonLauNhat)
                {
                    gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonLauNhat = gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai;
                }
            } 

            // Lưu dữ liệu vào file JSON
            QuanLyLuuTru.LuuGame(gameData);
            // Lưu dữ liệu vào PlayerPrefs để truy cập ở Scene Result
            PlayerPrefs.SetInt("soKillHienTai", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai);
            PlayerPrefs.SetFloat("thoiGianSinhTonHienTai", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai);
            PlayerPrefs.SetInt("soKillCaoNhat", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillCaoNhat);
            PlayerPrefs.SetFloat("thoiGianSinhTonLauNhat", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonLauNhat);
        }

        // Chuyển đổi sang scene Kết quả
        SceneManager.LoadScene("Result");

        // Xóa đối tượng hiện tại
        Destroy(gameObject);
    }

}
