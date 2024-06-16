using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HienThiCapDo : MonoBehaviour
{
    QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    public TextMeshProUGUI textCapDo;
    void Start()
    {
        // Kiem tra quanLyThongSoNhanVat vao Inspector chua
        if (quanLyThongSoNhanVat == null)
        {
            Debug.LogError("Chưa gán đối tượng QuanLyThongSoNhanVat vào HienThiCapDo!");
            return;
        }
    }

    void Update()
    {
       
        textCapDo.text = "Cấp độ: " + quanLyThongSoNhanVat.CapDoNhanVat;
    }
}
