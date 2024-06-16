using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HienThiKetQua : MonoBehaviour
{
    public TextMeshProUGUI KetquaSoLuongKill;
    public TextMeshProUGUI ThoiGianTraiQua;
    void Start()
    {
        // Lay So Luong kills Tu PlayerPrefs
        int kills = PlayerPrefs.GetInt("Kills", 0);
        KetquaSoLuongKill.text = "Số Quái đã bị hạ: " + kills;

        // Lay So Luong kills Tu PlayerPrefs
        float ThoiGianChoi = PlayerPrefs.GetFloat("Time", 0f);
        int minutes = Mathf.FloorToInt(ThoiGianChoi / 60);
        int seconds = Mathf.FloorToInt(ThoiGianChoi % 60);
        ThoiGianTraiQua.text = string.Format("Thời gian tồn tại: {0:00}:{1:00}", minutes, seconds);
    }

}
