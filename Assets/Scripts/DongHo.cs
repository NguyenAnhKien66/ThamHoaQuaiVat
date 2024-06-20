using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DongHo : MonoBehaviour
{
    public TextMeshProUGUI DongHotxt;  
    private float ThoiGianTroiQua;  

    void Start()
    {
        ThoiGianTroiQua = 0f;  
    }

    void Update()
    {
        ThoiGianTroiQua += Time.deltaTime;  

        int minutes = Mathf.FloorToInt(ThoiGianTroiQua / 60); 
        int seconds = Mathf.FloorToInt(ThoiGianTroiQua % 60);  

        
        DongHotxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public float GetThoiGianTroiQua()
    {
        return ThoiGianTroiQua;
    }
}
