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
           /* SceneManager.LoadScene("Result");*/
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.CompareTag("Player"))
        {         
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
