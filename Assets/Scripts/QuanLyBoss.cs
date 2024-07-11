using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuanLyBoss : MonoBehaviour
{
    NhanVat NhanVat;
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    public int LuongMau;
    public bool AnimationTanCong;
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    private float ThoiGianBatDau; 
    private bool DuyNhat = false; 
    public BossAI bossAI;
    public QuaiBossAI quaiBossAI;
    void Start()
    {
        ThoiGianBatDau = 0f; //n
    }

    private void Update()
    {
        ThoiGianBatDau += Time.deltaTime;
        if (ThoiGianBatDau >= 300f && DuyNhat == false)
        {
            TangChiSoBoss();
            DuyNhat = true;
        }
    }
    private void TangChiSoBoss()
    {
        SatThuongLonNhat += 10;
        SatThuongNhoNhat += 10;
        if (bossAI != null)
        {
            bossAI.TocDoDiChuyen = 6f;
        }

        if (quaiBossAI != null)
        {
            quaiBossAI.TocDoDiChuyen = 6f;
        }
    }
    public void SatThuongBossGanhChieu(int SatThuong)
    {
        LuongMau-=SatThuong;
        if(LuongMau <=0)
        {
            Destroy(gameObject);
            /*DuLieuGame gameData = DieuKhienGame.instance.duLieuGame;
            if (gameData != null && gameData.maps.Length > 0)
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

               
                gameData.maps[currentSceneIndex].DaHaDuocboss = true; 
                // Lưu dữ liệu vào file JSON
                QuanLyLuuTru.LuuGame(gameData);

            }*/
            DuLieuGame gameData = DieuKhienGame.instance.duLieuGame;
            if (gameData != null && gameData.maps.Length > 0)
            {
                // Lưu số lượng kills
                if (DemQuaiChet.instance != null)
                {
                    gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai = DemQuaiChet.instance.LaySoLuongHienTai();
                    if (gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai >= gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillCaoNhat)
                    {
                        gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillCaoNhat = gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai;
                    }
                }

                // Lưu thời gian trôi qua
                DongHo dongHo = FindObjectOfType<DongHo>();
                if (dongHo != null)
                {
                    gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai = dongHo.GetThoiGianTroiQua();
                    if (gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai >= gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonLauNhat)
                    {
                        gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonLauNhat = gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai;
                    }
                }
                gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].DaHaDuocboss = true;
                gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapHienTai = quanLyThongSoNhanVat.CapDoNhanVat;
                if (gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapHienTai >= gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapCaoNhat)
                {
                    gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapCaoNhat = gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapHienTai;
                }
                // Lưu dữ liệu vào file JSON
                QuanLyLuuTru.LuuGame(gameData);
                // Lưu dữ liệu vào PlayerPrefs để truy cập ở Scene Result
                PlayerPrefs.SetInt("soKillHienTai", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillHienTai);
                PlayerPrefs.SetFloat("thoiGianSinhTonHienTai", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonHienTai);
                PlayerPrefs.SetInt("soKillCaoNhat", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].soKillCaoNhat);
                PlayerPrefs.SetFloat("thoiGianSinhTonLauNhat", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].thoiGianSinhTonLauNhat);
                PlayerPrefs.SetInt("CapCaoNhat", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapCaoNhat);
                PlayerPrefs.SetInt("CapHienTai", gameData.maps[SceneManager.GetActiveScene().buildIndex - 1].CapHienTai);
            }

            SceneManager.LoadScene("Result");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // animation tan cong
            if (GetComponent<Animator>() != null && AnimationTanCong)
            {
                GetComponent<Animator>().SetBool("VaCham", true);
            }
            NhanVat = collision.GetComponent<NhanVat>();
            if(NhanVat != null)
            {
                InvokeRepeating("SatThuongBossGayRa", 0f, 0.1f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Ngung animation tan cong
            if (GetComponent<Animator>() != null && AnimationTanCong)
            {
                GetComponent<Animator>().SetBool("VaCham", false);
            }
            NhanVat = null;
            CancelInvoke("SatThuongBossGayRa");
        }
    }
    void SatThuongBossGayRa()
    {
        int SatThuong=UnityEngine.Random.Range(SatThuongNhoNhat,SatThuongLonNhat);
        NhanVat.SatThuongGanhChieu(SatThuong);
    }


}
