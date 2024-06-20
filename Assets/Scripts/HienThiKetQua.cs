using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HienThiKetQua : MonoBehaviour
{
    public TextMeshProUGUI KetquaSoLuongKill;
    public TextMeshProUGUI ThoiGianTraiQua;
    public TextMeshProUGUI Capdo;
    void Start()
    {
        // Lay So Luong kills Tu PlayerPrefs
        int kills = PlayerPrefs.GetInt("Kills", 0);
        KetquaSoLuongKill.text = "Số Quái đã bị hạ: " + kills;

        // Lay So Luong kills Tu PlayerPrefs
        float ThoiGianChoi = PlayerPrefs.GetFloat("Time", 0f);
        int Phut = Mathf.FloorToInt(ThoiGianChoi / 60);
        int Giay = Mathf.FloorToInt(ThoiGianChoi % 60);
        ThoiGianTraiQua.text = string.Format("Thời gian tồn tại: {0:00}:{1:00}", Phut, Giay);

        // Lay So Luong Level Tu PlayerPrefs
        int level = PlayerPrefs.GetInt("Level", 1);
        Capdo.text = "Cấp độ " + level;
    }

}
