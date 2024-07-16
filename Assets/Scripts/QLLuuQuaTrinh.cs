using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QLLuuQuaTrinh : MonoBehaviour
{
    public GameObject HopThoaiLuu;
    public Button NutCo;
    public Button NutKhong;
    //Đối tượng lưu dữ liệu
    public GameObject NhanVatG;
    public NhanVat NhanVatS;
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;


    private bool ChoThoat = false;
    private bool HienHopThoaiLuu = false;
    // Start is called before the first frame update
    void Start()
    {
        HopThoaiLuu.SetActive(false);

        NutCo.onClick.AddListener(LuuVaThoatGame);
        NutKhong.onClick.AddListener(KhongLuuVaThoatGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (HienHopThoaiLuu)
        {
            HopThoaiLuu.SetActive(true);
            HienHopThoaiLuu = false;
        }
    }
    void OnApplicationQuit()
    {
        if (!ChoThoat)
        {
            HienHopThoaiLuu = true;
            Application.CancelQuit();
        }
    }
    void LuuVaThoatGame()
    {
        //Thực hiện lưu tại đây
        LuuTrangThaiGame();
        //Thoát sau khi lưu
        ChoThoat = true;
        Application.Quit();
    }
    void KhongLuuVaThoatGame()
    {
        //Thoát mà không lưu
        ChoThoat = true;
        Application.Quit();
    }
    void LuuTrangThaiGame()
    {
        GameData gameData = new GameData(); 
        //Lưu màn hình map
        gameData.ViTriManHinh = SceneManager.GetActiveScene().buildIndex;

        //Lưu vị trí của người chơi
        gameData.ViTriNhanVat = NhanVatG.transform.position;

        //Lưu lượng máu hiện tại
        gameData.MauNVHienTai = quanLyThongSoNhanVat.MauHientai;

        //Lưu thanh hồi chiêu
        gameData.ThoiGianHoiChieuNV = NhanVatS.ThoiGianHoiChieu;
        gameData.HoiChieuCuonNV = NhanVatS.HoiChieuCuon;
        
        //Lưu lượng giáp hiện tại
        gameData.GiapNVHienTai = quanLyThongSoNhanVat.GiapHienTai;

        //Lưu sát thương nhân vật
        gameData.SatThuongNhoNhatNV = quanLyThongSoNhanVat.SatThuongNhoNhat;
        gameData.SatThuongLonNhatNV = quanLyThongSoNhanVat.SatThuongLonNhat;

        //Lưu số kill
        if (DemQuaiChet.instance != null)
        {
            gameData.SoKillNV = DemQuaiChet.instance.LaySoLuongHienTai();
        }
        //Lưu cấp độ và kinh nghiệm
        gameData.CapDoNV = quanLyThongSoNhanVat.CapDoNhanVat;
        gameData.KinhNghiemNV = NhanVatS.kinhNghiemNhanVat;

        //Lưu thời gian
        DongHo DongHo = FindObjectOfType<DongHo>();
        if (DongHo != null)
        {
            gameData.TGSinhTon = DongHo.GetThoiGianTroiQua();
            
        }

        // Lưu trạng thái của các quái
        gameData.CacQuai = new List<EnemyData>();
/*        bool CoQuai = false;*/
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("QuaiVat"))
        {
            if (enemy != null)
            {
                Vector3 position = enemy.transform.position;
                int mau = enemy.GetComponent<QuanLyQuai>().LuongMau;
                string tenQuaiPrefabs = GetPrefabNameWithoutExtension(enemy.name);
                gameData.CacQuai.Add(new EnemyData(position, mau, tenQuaiPrefabs));
            }
        }

        //Lưu item rơi
        gameData.CacItem = new List<ItemData>();
        /*        bool CoItem = false;*/
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("VatPham"))
        {
            if (item != null)
            {
                Vector3 positem = item.transform.position;
                string tenItem = GetPrefabNameWithoutExtension(item.name);
                gameData.CacItem.Add(new ItemData(positem, tenItem));
            }
        }

        // Lưu trạng thái của boss
        GameObject BossG = GameObject.FindWithTag("Boss");
        QuanLyBoss BossS = FindObjectOfType<QuanLyBoss>();
        if (BossG != null && BossS != null)
        {
            Vector3 position = BossG.transform.position;
            gameData.Boss = new BossData(position, BossS.LuongMau, GetPrefabNameWithoutExtension(BossG.name));
            PlayerPrefs.SetInt("CoDuLieuBoss", 1); //Có dữ liệu
        }
        else
        {
            PlayerPrefs.SetInt("CoDuLieuBoss", 0); //Không có dữ liệu
        }
        //Lưu trạng thái điểm buff
        GameObject obj = GameObject.Find("DiemBuff");
        if (obj == null) //Điểm buff đã bị nhặt mất
        {
            PlayerPrefs.SetInt("TonTaiDiemBuff", 0);
        }
        else //Điểm buff vẫn còn
        {
            PlayerPrefs.SetInt("TonTaiDiemBuff", 1);
        }
        //Lưu dư liệu vào json
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);


        PlayerPrefs.Save(); // Đảm bảo dữ liệu được lưu ngay lập tức

        Debug.Log("Game đã được lưu!");
    }
    string GetPrefabNameWithoutExtension(string fullName)
    {
        // Xóa phần mở rộng .prefab nếu có
        int index = fullName.LastIndexOf(".prefab");
        if (index != -1)
        {
            return fullName.Substring(0, index);
        }
        return fullName;
    }
    [Serializable]
    public class GameData
    {
        public int ViTriManHinh;
        public Vector3 ViTriNhanVat;
        public int MauNVHienTai;
        public float ThoiGianHoiChieuNV;
        public float HoiChieuCuonNV;
        public int GiapNVHienTai;
        public int SatThuongNhoNhatNV;
        public int SatThuongLonNhatNV;
        public int SoKillNV;
        public int CapDoNV;
        public int KinhNghiemNV;
        public float TGSinhTon;
        public List<EnemyData> CacQuai;
        public List<ItemData> CacItem;
        public BossData Boss;
        public bool TonTaiDiemBuff;
    }
    [System.Serializable]
    public class EnemyData
    {
        public Vector3 position;
        public int mau;
        public string tenQuaiPrefabs;

        public EnemyData(Vector3 position, int mau, string tenQuaiPrefabs)
        {
            this.position = position;
            this.mau = mau;
            this.tenQuaiPrefabs = tenQuaiPrefabs;
        }
    }
    [System.Serializable]
    public class ItemData
    {
        public Vector3 position;
        public string tenItem;

        public ItemData(Vector3 position, string tenItem)
        {
            this.position = position;
            this.tenItem = tenItem;
        }
    }
    [Serializable]
    public class BossData
    {
        public Vector3 position;
        public int mauBoss;
        public string tenBossPrefabs;

        public BossData(Vector3 position, int mauBoss, string tenBossPrefabs)
        {
            this.position = position;
            this.mauBoss = mauBoss;
            this.tenBossPrefabs = tenBossPrefabs;
        }
    }



}
