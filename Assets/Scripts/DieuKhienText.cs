using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DieuKhienText : MonoBehaviour
{
    public TextMeshProUGUI ThangCapTxt;
    public TextMeshProUGUI CongDameTxt;
    public TextMeshProUGUI CongMauTxt;
    public TextMeshProUGUI CongGiapTxt;
    public float ThoiGianHienThi = 2.0f; // Thời gian hiển thị text
    
    void Start()
    {
        //Ẩn text khi bắt đầu
        ThangCapTxt.gameObject.SetActive(false);
        CongDameTxt.gameObject.SetActive(false);
        CongMauTxt.gameObject.SetActive(false); 
        CongGiapTxt.gameObject.SetActive(false);
    }
    //Hiện Thăng Cấp Txt
    public void HienThiThangCapTxt()
    {
        //Hiển thị text
        ThangCapTxt.gameObject.SetActive(true);
        // Bắt đầu coroutine để ẩn văn bản sau một khoảng thời gian
        StartCoroutine(AnThangCapTxtSauTG(ThoiGianHienThi));
    }    
    private IEnumerator AnThangCapTxtSauTG(float thoigian)
    {
        yield return new WaitForSeconds(thoigian);
        //Ẩn text
        ThangCapTxt.gameObject.SetActive(false);   

    }
    //Hiện Cộng Dame Txt
    public void HienThiCongDameTxt(int dameduoccong)
    {
        //Hiển thị text
        CongDameTxt.text = "+" + dameduoccong.ToString() + " sát thương";
        CongDameTxt.gameObject.SetActive(true);
        
        
        // Bắt đầu coroutine để ẩn văn bản sau một khoảng thời gian
        StartCoroutine(AnCongDameTxtSauTG(ThoiGianHienThi));
    }
    private IEnumerator AnCongDameTxtSauTG(float thoigian)
    {
        yield return new WaitForSeconds(thoigian);
        //Ẩn text
        CongDameTxt.gameObject.SetActive(false);

    }
    //Hiện Cộng Máu Txt
    public void HienThiCongMauTxt(int mauduoccong)
    {
        //Hiển thị text
        CongMauTxt.text = "+" + mauduoccong.ToString() + " máu";
        CongMauTxt.gameObject.SetActive(true);
        
        // Bắt đầu coroutine để ẩn văn bản sau một khoảng thời gian
        StartCoroutine(AnCongMauTxtSauTG(ThoiGianHienThi));
    }
    private IEnumerator AnCongMauTxtSauTG(float thoigian)
    {
        yield return new WaitForSeconds(thoigian);
        //Ẩn text
        CongMauTxt.gameObject.SetActive(false);

    }
    //Hiện Cộng Giáp Txt
    public void HienThiCongGiapTxt(int giapduoccong)
    {
        //Hiển thị text
        CongGiapTxt.text = "+" + giapduoccong.ToString() + " giáp";
        CongGiapTxt.gameObject.SetActive(true);

        // Bắt đầu coroutine để ẩn văn bản sau một khoảng thời gian
        StartCoroutine(AnCongGiapTxtSauTG(ThoiGianHienThi));
    }
    private IEnumerator AnCongGiapTxtSauTG(float thoigian)
    {
        yield return new WaitForSeconds(thoigian);
        //Ẩn text
        CongGiapTxt.gameObject.SetActive(false);

    }

}
