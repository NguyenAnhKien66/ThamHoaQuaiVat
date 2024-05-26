using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyQuai : MonoBehaviour
{
    NhanVat nhanVat;
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    ThanhMauNhanVat thanhMauQuai;
    private void Start()
    {
        thanhMauQuai = GetComponent<ThanhMauNhanVat>();
    }
    public void SatThuongQuaiGanhChieu(int SatThuong)
    {
        thanhMauQuai.NhanSatThuong(SatThuong);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nhanVat = collision.GetComponent<NhanVat>();
            InvokeRepeating("SatThuongQuaigayRa", 0,0.1f);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nhanVat = null;
            CancelInvoke("SatThuongCuaNhanVat");    

        }
    }

    void SatThuongQuaigayRa()
    {
        int SatThuong= UnityEngine.Random.Range(SatThuongNhoNhat, SatThuongLonNhat);
        Debug.Log("Player take damage "+SatThuong);
        nhanVat.SatThuongGanhChieu(SatThuong);

    }
}
