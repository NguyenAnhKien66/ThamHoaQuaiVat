using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThanhMau : MonoBehaviour
{
    // Start is called before the first frame update

    public Image LuongMau;
    public TextMeshProUGUI Mautxt;
    public void CapnhatMau(int LuongMauHienTai, int LuongMauToiDa)
    {
        LuongMau.fillAmount=(float)LuongMauHienTai/ (float)LuongMauToiDa;
        Mautxt.text= LuongMauHienTai.ToString()+" / "+LuongMauToiDa.ToString();


    }

 
}
