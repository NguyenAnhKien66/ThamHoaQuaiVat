using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dan : MonoBehaviour
{
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    public bool CoPhaiNhanVat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !CoPhaiNhanVat)
        {
            int SatThuongTuQuai = Random.Range(SatThuongNhoNhat, SatThuongLonNhat);
            Debug.Log(SatThuongTuQuai);
            collision.GetComponent<NhanVat>().SatThuongGanhChieu(SatThuongTuQuai);
            Destroy(gameObject);
        }
        if (collision.CompareTag("QuaiVat") && CoPhaiNhanVat)
        {
            int SatThuongTuNhanVat = Random.Range(SatThuongNhoNhat, SatThuongLonNhat);
            collision.GetComponent<QuanLyQuai>().SatThuongQuaiGanhChieu(SatThuongTuNhanVat);
            Destroy(gameObject);
        }
    }
}
