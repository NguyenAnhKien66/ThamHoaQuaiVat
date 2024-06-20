using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemQuaiChet : MonoBehaviour
{
    public static DemQuaiChet instance; // Bien static de dam bao co mot instance duy nhat
    public TextMeshProUGUI DemQuaiChettxt;
    private int DemSoLuongQuaiChet;
    private void Start()
    {
        CapNhatSoLuongQuaiChet();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Dam bao object nay khong bi huy khi chuyen scene
        }
        else
        {
            Destroy(gameObject);// Neu da co instance khac, huy gameObject nay
        }
    }
    public void ThemSoluong()
    {
        DemSoLuongQuaiChet++;
        CapNhatSoLuongQuaiChet();
        
    }
    private void CapNhatSoLuongQuaiChet()
    {
        if (DemQuaiChettxt != null)
        {
            DemQuaiChettxt.text = "Kills: " + DemSoLuongQuaiChet;
        }
    }
    public int LaySoLuongHienTai()
    {
        return DemSoLuongQuaiChet;
    }
    public void ThietLapSoLuong(int dem)
    {
        DemSoLuongQuaiChet= dem;
        CapNhatSoLuongQuaiChet();
    }
}
