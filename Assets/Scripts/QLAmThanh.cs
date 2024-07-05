using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmThanh
{
    public static bool bAmThanh = true;
   
}
public class QLAmThanh : MonoBehaviour
{
    public GameObject BatAm;
    public GameObject TatAm;
    public GameObject NhacNen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quản lý việc Ẩn hiện nút bật tắt dựa vào trạng thái âm thanh
        if (AmThanh.bAmThanh == true)
        {
            if (BatAm != null && TatAm != null)
            {
                BatAm.SetActive(true);
                TatAm.SetActive(false);
            }
            if (NhacNen != null)
            {
                NhacNen.SetActive(true);
            }

        }
        else
        {
            if (BatAm != null && TatAm != null)
            {
                BatAm.SetActive(false);
                TatAm.SetActive(true);
            }
            if (NhacNen != null)
            {
                NhacNen.SetActive(false);
            }
        }
    }
}
