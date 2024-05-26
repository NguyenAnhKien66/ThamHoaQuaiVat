using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public void NhanSatThuong(int SatThuong) {
        LuongMauHienTai -= SatThuong;
        if(LuongMauHienTai <= 0) {
            LuongMauHienTai = 0;

            SauKhiMatMang.Invoke();
        }
        thanhMau.CapnhatMau(LuongMauHienTai, LuongMauToiDa);
    }
    public void Matmang()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
       
    }

}
