using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyQuai : MonoBehaviour
{
    NhanVat nhanVat;
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    public int LuongMau;
    public bool AnimationTanCong;

    void Awake()
    {
        LuongMau = 100;
    }

    private void Start()
    {
        
    }

    public void SatThuongQuaiGanhChieu(int SatThuong)
    {
        LuongMau -= SatThuong;
        Debug.Log("Quái nhận sát thương " + SatThuong + ". Lượng máu còn lại: " + LuongMau);

        if (LuongMau <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //animation quái đánh
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
            //Ngừng animation quái đánh
            if (GetComponent<Animator>() != null&& AnimationTanCong)
            {
                GetComponent<Animator>().SetBool("VaCham", false);
            }
             
            CancelInvoke("SatThuongQuaigayRa");
            nhanVat = null;
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
