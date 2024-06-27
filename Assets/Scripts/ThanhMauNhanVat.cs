using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 

public class ThanhMauNhanVat : MonoBehaviour
{
    [SerializeField] QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    
    public ThanhMau thanhMau;

    public UnityEvent SauKhiMatMang;

    private void Start()
    {
        quanLyThongSoNhanVat.MauHientai = quanLyThongSoNhanVat.MauToiDaNhanVat; 
        thanhMau.CapnhatMau(quanLyThongSoNhanVat.MauHientai, quanLyThongSoNhanVat.MauToiDaNhanVat);
    }

    private void OnEnable()
    {
        SauKhiMatMang.AddListener(Matmang);
    }

    private void OnDisable()
    {
        SauKhiMatMang.RemoveListener(Matmang);
    }

    public void NhanSatThuong(int SatThuong)
    {
        quanLyThongSoNhanVat.MauHientai -= SatThuong;

        Debug.Log("co tru mau");
        if (quanLyThongSoNhanVat.MauHientai <= 0)
        {
            quanLyThongSoNhanVat.MauHientai = 0;
            SauKhiMatMang.Invoke();
        }
        thanhMau.CapnhatMau(quanLyThongSoNhanVat.MauHientai, quanLyThongSoNhanVat.MauToiDaNhanVat);
    }

    public void Matmang()
    {
        // Lay so luong kills va chuyen scene
        if (DemQuaiChet.instance != null)
        {
            PlayerPrefs.SetInt("Kills", DemQuaiChet.instance.LaySoLuongHienTai());
            PlayerPrefs.Save(); 
        
        }
        DongHo dongHo = FindObjectOfType<DongHo>();
        if (dongHo != null)
        {
            PlayerPrefs.SetFloat("Time", dongHo.GetThoiGianTroiQua());
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("Level", GetComponent<NhanVat>().quanLyThongSoNhanVat.CapDoNhanVat);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Result");
        Destroy(gameObject);
    }

    private void Update()
    {
        // Any other update logic you need
    }
}
