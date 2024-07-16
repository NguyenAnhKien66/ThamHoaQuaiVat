using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static QLLuuQuaTrinh;

public class ChucNangTiepTucQT : MonoBehaviour
{
    public GameObject TiepTucQT; //Nút Tiếp Tục
    public GameObject NhanVatG;
    public NhanVat NhanVatS;
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    public ThanhMau thanhMau;
    public ThanhGiap thanhGiap;
    public GameObject ConTro;

    // Start is called before the first frame update
    void Start()
    {
        /*ResetDuLieu();*/
        if (TiepTucQT != null)
        {
            // Kiểm tra xem game đã được lưu hay chưa
            if (File.Exists(Application.persistentDataPath + "/savefile.json"))
            {
                // Hiển thị nút Tiếp tục
                TiepTucQT.SetActive(true);
            }
            else
            {
                TiepTucQT.SetActive(false);
            }
        }
        int TiepTuc = PlayerPrefs.GetInt("TiepTuc");
        if (TiepTuc == 1 && NhanVatG != null)
        {
            // Khôi phục trạng thái game sau khi scene được tải lại
            StartCoroutine(KhoiPhucTrangThaiGame());

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TiepTucGame()
    {

        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            PlayerPrefs.SetInt("TiepTuc", 1);
            // Khôi phục scene đã lưu
            string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            SceneManager.LoadScene(gameData.ViTriManHinh);

        }
        else
        {
            Debug.LogWarning("Không có dữ liệu để khôi phục!");
        }
    }
    IEnumerator KhoiPhucTrangThaiGame()
    {
        yield return null;
        //Khôi phục vị trí nhân vật
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            NhanVatG.transform.position = gameData.ViTriNhanVat;
            //Khôi phục lượng máu hiện tại
            quanLyThongSoNhanVat.MauHientai = gameData.MauNVHienTai;
            thanhMau.CapnhatMau(quanLyThongSoNhanVat.MauHientai, quanLyThongSoNhanVat.MauToiDaNhanVat);
            //Khôi phục thanh hồi chiêu
            NhanVatS.ThoiGianHoiChieu = gameData.ThoiGianHoiChieuNV;
            NhanVatS.HoiChieuCuon = gameData.HoiChieuCuonNV;
            ThanhKyNangNhanVat thanhKyNangNhanVat = FindObjectOfType<ThanhKyNangNhanVat>();
            thanhKyNangNhanVat.CapNhatHoiChieu(1 - NhanVatS.ThoiGianHoiChieu / NhanVatS.HoiChieuCuon);
            //Khôi phục lượng giáp hiện tại
            quanLyThongSoNhanVat.GiapHienTai = gameData.GiapNVHienTai;
            thanhGiap.CapnhatGiap(quanLyThongSoNhanVat.GiapHienTai, quanLyThongSoNhanVat.GiapToiDa);
            //Khôi phục sát thương của nhân vật      
            quanLyThongSoNhanVat.SatThuongNhoNhat = gameData.SatThuongNhoNhatNV;
            quanLyThongSoNhanVat.SatThuongLonNhat = gameData.SatThuongLonNhatNV;
            //Khôi phục số kill 
            DemQuaiChet.instance.DemSoLuongQuaiChet = gameData.SoKillNV;
            DemQuaiChet.instance.CapNhatSoLuongQuaiChet();

            //Khôi phục cấp độ và kinh nghiệm             
            quanLyThongSoNhanVat.CapDoNhanVat = gameData.CapDoNV;
            NhanVatS.kinhNghiemNhanVat = gameData.KinhNghiemNV;
            NhanVatS.CapNhatUI();

            //Khôi phục thời gian
            DongHo DongHo = FindObjectOfType<DongHo>();
            if (DongHo != null)
            {

                DongHo.ThoiGianTroiQua = gameData.TGSinhTon;
            }
            // Khôi phục trạng thái của các quái
            foreach (var enemyData in gameData.CacQuai)
            {
                //Tạo quái dựa vào tên quái prefabs đã lưu
                string tenQuaiPrefabs = enemyData.tenQuaiPrefabs.Replace("(Clone)", "").Trim();
                GameObject quaiPrefab = Resources.Load<GameObject>("AllQuai/" + tenQuaiPrefabs);
                if (quaiPrefab != null)
                {
                    GameObject enemy = Instantiate(quaiPrefab, enemyData.position, Quaternion.identity);
                    QuanLyQuai quanLyQuai = enemy.GetComponent<QuanLyQuai>();
                    if (quanLyQuai != null)
                    {
                        quanLyQuai.LuongMau = enemyData.mau;
                    }
                }
            }
            //Khôi phục item rơi

            foreach (var itemData in gameData.CacItem)
            {
                string tenItemPrefabs = itemData.tenItem.Replace("(Clone)", "").Trim();
                GameObject itemPrefab = Resources.Load<GameObject>("Item/" + tenItemPrefabs);
                if (itemPrefab != null)
                {
                    Instantiate(itemPrefab, itemData.position, Quaternion.identity);
                }
            }

            //Khôi phục trạng thái boss
            if (PlayerPrefs.GetInt("CoDuLieuBoss") == 1) //Có dữ liệu boss thì mới khôi phục
            {
                if (gameData.Boss != null)
                {
                    string tenBossDaChinh = gameData.Boss.tenBossPrefabs.Replace("(Clone)", "").Trim();
                    GameObject bossPrefab = Resources.Load<GameObject>("AllQuai/" + tenBossDaChinh);
                    if (bossPrefab != null)
                    {
                        GameObject boss = Instantiate(bossPrefab, gameData.Boss.position, Quaternion.identity);
                        QuanLyBoss quanLyBoss = boss.GetComponent<QuanLyBoss>();
                        if (quanLyBoss != null)
                        {
                            quanLyBoss.LuongMau = gameData.Boss.mauBoss;
                        }
                    }
                }
            }

            //Khôi phục trạng thái điểm buff
            GameObject DiemBuff = GameObject.Find("DiemBuff");

            if (DiemBuff != null)
            {
                if (PlayerPrefs.GetInt("TonTaiDiemBuff") == 0)
                {
                    Destroy(DiemBuff.gameObject);
                    ConTro.SetActive(false);
                }
            }
        }
    }
    void ResetDuLieu()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Đảm bảo rằng dữ liệu bị xóa ngay lập tức
        Debug.Log("Tất cả dữ liệu PlayerPrefs đã được reset.");
    }

    
}
