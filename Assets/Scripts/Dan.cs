using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dan : MonoBehaviour
{
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    
    public bool CoPhaiNhanVat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !CoPhaiNhanVat)
        {
            int SatThuongTuQuai = Random.Range(quanLyThongSoNhanVat.SatThuongNhoNhatQuai, quanLyThongSoNhanVat.SatThuongNhoNhatQuai);
            Debug.Log(SatThuongTuQuai);
            collision.GetComponent<NhanVat>().SatThuongGanhChieu(SatThuongTuQuai);
            Destroy(gameObject);
        }
        if (collision.CompareTag("QuaiVat") && CoPhaiNhanVat)
        {
            int SatThuongQuaigayRa = Random.Range(quanLyThongSoNhanVat.SatThuongNhoNhat, quanLyThongSoNhanVat.SatThuongLonNhat);
            collision.GetComponent<QuanLyQuai>().SatThuongQuaiGanhChieu(SatThuongQuaigayRa);
            Destroy(gameObject);
        }
    }
}
