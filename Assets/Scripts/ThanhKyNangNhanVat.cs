using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThanhKyNangNhanVat : MonoBehaviour
{
    public Image HoiChieuKyNang;
    public void CapNhatHoiChieu(float HoiChieu)
    {
        HoiChieuKyNang.fillAmount = HoiChieu;
    }
}
