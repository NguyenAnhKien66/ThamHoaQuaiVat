using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConTro : MonoBehaviour
{
    public Transform NhanVat;
    public Transform MucTieu;
    public Transform ConTroObj;
    GameObject obj;
    
    void Update()
    {
        HuongToiMucTieu();

    }
    void HuongToiMucTieu()
    {
        //Tính toán hướng từ nhân vật tới mục tiêu
        Vector3 Huong=MucTieu.position-NhanVat.position;

        //Tính toán góc quay
        float Goc=Mathf.Atan2(Huong.y, Huong.x)*Mathf.Rad2Deg;

        //Xoay con trỏ về hướng mục tiêu
        ConTroObj.rotation=Quaternion.Euler(new Vector3(0,0,Goc));

    }    
   
}
