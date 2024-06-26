using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // Add this line

public class ThanhMauNhanVat : MonoBehaviour
{
    [SerializeField] int LuongMauToiDa;
    int LuongMauHienTai;
    public ThanhMau thanhMau;

    public UnityEvent SauKhiMatMang;

    private void Start()
    {
        LuongMauHienTai = LuongMauToiDa;
        thanhMau.CapnhatMau(LuongMauHienTai, LuongMauToiDa);
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
        LuongMauHienTai -= SatThuong;
        if (LuongMauHienTai <= 0)
        {
            LuongMauHienTai = 0;
            SauKhiMatMang.Invoke();
        }
        thanhMau.CapnhatMau(LuongMauHienTai, LuongMauToiDa);
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
        SceneManager.LoadScene("Result");
        Destroy(gameObject);
    }

    private void Update()
    {
        // Any other update logic you need
    }
}
