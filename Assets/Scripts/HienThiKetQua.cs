using UnityEngine;
using TMPro;

public class HienThiKetQua : MonoBehaviour
{
    public TextMeshProUGUI KetquaSoLuongKill;
    public TextMeshProUGUI ThoiGianTraiQua;
    public TextMeshProUGUI Capdo;
    public TextMeshProUGUI SoKillCaoNhat;
    public TextMeshProUGUI ThoiGianSinhTonLauNhat;
    public TextMeshProUGUI CapCaoNhat;

    void Start()
    {
        
        // Nếu không gán trong Inspector, hãy gán chúng tại đây trước khi sử dụng

        // Lay So Luong kills Hien Tai Tu PlayerPrefs
        int killsHienTai = PlayerPrefs.GetInt("soKillHienTai", 0);
        KetquaSoLuongKill.text = "Số Quái đã bị hạ: " + killsHienTai;

        // Lay thoi gian ton tai Hien Tai tu PlayerPrefs
        float thoiGianHienTai = PlayerPrefs.GetFloat("thoiGianSinhTonHienTai", 0f);
        int PhutHienTai = Mathf.FloorToInt(thoiGianHienTai / 60);
        int GiayHienTai = Mathf.FloorToInt(thoiGianHienTai % 60);
        ThoiGianTraiQua.text = string.Format("Thời gian tồn tại: {0:00}:{1:00}", PhutHienTai, GiayHienTai);

        // Lay Cap Hien Tai Tu PlayerPrefs
        int capHienTai = PlayerPrefs.GetInt("CapHienTai", 1);
        Capdo.text = "Cấp độ " + capHienTai;

        // Lay So Luong kills Cao Nhat Tu PlayerPrefs
        int killsCaoNhat = PlayerPrefs.GetInt("soKillCaoNhat", 0);
        SoKillCaoNhat.text = "Số Quái cao nhất: " + killsCaoNhat;

        // Lay thoi gian ton tai Cao Nhat tu PlayerPrefs
        float thoiGianCaoNhat = PlayerPrefs.GetFloat("thoiGianSinhTonLauNhat", 0f);
        int PhutCaoNhat = Mathf.FloorToInt(thoiGianCaoNhat / 60);
        int GiayCaoNhat = Mathf.FloorToInt(thoiGianCaoNhat % 60);
        ThoiGianSinhTonLauNhat.text = string.Format("Thời gian tồn tại cao nhất: {0:00}:{1:00}", PhutCaoNhat, GiayCaoNhat);

        // Lay Cap Cao Nhat Tu PlayerPrefs
        int capCaoNhat = PlayerPrefs.GetInt("CapCaoNhat", 1);
        CapCaoNhat.text = "Cấp độ cao nhất: " + capCaoNhat;
    }
}
