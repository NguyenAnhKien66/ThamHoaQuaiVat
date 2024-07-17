using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ThanhGiap : MonoBehaviour
{
    public Image LuongGiap;
    public TextMeshProUGUI Mautxt;
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    public void CapnhatGiap(int LuongGiapHienTai, int LuongGiapToiDa)
    {
        LuongGiap.fillAmount = (float)LuongGiapHienTai / (float)LuongGiapToiDa;
        Mautxt.text = LuongGiapHienTai.ToString() + " / " + LuongGiapToiDa.ToString();
    }
    public void NhanSatThuong(int SatThuong)
    {
        if (SatThuong >= quanLyThongSoNhanVat.GiapHienTai)
        {
            quanLyThongSoNhanVat.GiapHienTai = 0;
        }
        else
        {
            quanLyThongSoNhanVat.GiapHienTai -= SatThuong;
        }
        CapnhatGiap(quanLyThongSoNhanVat.GiapHienTai, quanLyThongSoNhanVat.GiapToiDa);
    }
}
