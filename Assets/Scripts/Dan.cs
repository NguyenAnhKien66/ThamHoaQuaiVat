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
            NhanVat nhanVat = collision.GetComponent<NhanVat>();
            if (nhanVat != null)
            {
                int SatThuongTuQuai = Random.Range(quanLyThongSoNhanVat.SatThuongNhoNhatQuai, quanLyThongSoNhanVat.SatThuongLonNhatQuai);
                Debug.Log(SatThuongTuQuai);
                nhanVat.SatThuongGanhChieu(SatThuongTuQuai);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("QuaiVat") && CoPhaiNhanVat)
        {
            NhanVat nhanVat = GameObject.FindGameObjectWithTag("Player").GetComponent<NhanVat>(); // Luôn tìm đối tượng nhanVat
            QuanLyQuai quaiVat = collision.GetComponent<QuanLyQuai>();
            if (quaiVat != null && nhanVat != null)
            {
                int SatThuongQuaigayRa = Random.Range(quanLyThongSoNhanVat.SatThuongNhoNhat, quanLyThongSoNhanVat.SatThuongLonNhat);
                quaiVat.SatThuongQuaiGanhChieu(SatThuongQuaigayRa, nhanVat);
                Destroy(gameObject);
            }
        }
    }
}
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
        if (collision.CompareTag("Boss") && CoPhaiNhanVat)
        {
            int SatThuongTuNhanVat = Random.Range(SatThuongNhoNhat, SatThuongLonNhat);
            collision.GetComponent<QuanLyBoss>().SatThuongBossGanhChieu(SatThuongTuNhanVat);
            Destroy(gameObject);
        }

    }
}
