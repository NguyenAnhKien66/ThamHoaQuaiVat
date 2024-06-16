using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="QuanLyThongSoNhanVat",menuName = "ScriptableObjects/QuanLyThongSoNhanVat",order =1)]
public class QuanLyThongSoNhanVat : ScriptableObject
{
    public float TocDoNhanVat = 5f;
    public float TocDoCuonNhanVat = 2f;
    public float ThoiGianCuonNhanVat = 1f;
    public float HoiChieuCuonNhanVat = 10f;
    public int MauToiDaNhanVat = 100;

    // Sat Thuong Gay ra tu nhan Vat
    public int SatThuongNhoNhat;
    public int SatThuongLonNhat;
    // Sat Thuong Gay ra tu Quai
    public int SatThuongNhoNhatQuai;
    public int SatThuongLonNhatQuai;
    // Thong so sung
    public float TocDoban = 1f; // Toc do ban
    public float LucBan = 10f; // Luc ban

    // Thong so cap do
    public int KinhNghiemToiDa = 100;
    public int SatThuongCongThemKhiThangCap = 5;
    public int CapDoNhanVat = 1;
}
