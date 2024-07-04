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

    void Start()
    {
        
    }
    public void SatThuongBossGanhChieu(int SatThuong)
    {
        LuongMau-=SatThuong;
        if(LuongMau <=0)
        {
            Destroy(gameObject);
            DuLieuGame gameData = DieuKhienGame.instance.duLieuGame;
            if (gameData != null && gameData.maps.Length > 0)
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

                // Lưu thông tin đã hạ được boss
                gameData.maps[currentSceneIndex].DaHaDuocboss = true; 
                // Lưu dữ liệu vào file JSON
                QuanLyLuuTru.LuuGame(gameData);

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
