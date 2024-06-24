using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiemBuff : MonoBehaviour
{
    [SerializeField] GameObject ConTro;
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            quanLyThongSoNhanVat.SatThuongLonNhat += quanLyThongSoNhanVat.SatThuongCongThemKhiThangCap;
            quanLyThongSoNhanVat.SatThuongNhoNhat += quanLyThongSoNhanVat.SatThuongCongThemKhiThangCap;
            Destroy(gameObject);
            ConTro.SetActive(false);
        }    
    }
}
