using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            if (PlayerPrefs.GetInt("DaLuuGame", 0) == 1)
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
        if (TiepTuc == 1&& NhanVatG != null)
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
       
        if(PlayerPrefs.GetInt("DaLuuGame",0)==1)
        {
            PlayerPrefs.SetInt("TiepTuc", 1);
            // Khôi phục scene đã lưu
            int ViTriManHinh = PlayerPrefs.GetInt("ViTriManHinh");
            SceneManager.LoadScene(ViTriManHinh);
            
            
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
        float nhanVatX = PlayerPrefs.GetFloat("NhanVatX");
        float nhanVatY = PlayerPrefs.GetFloat("NhanVatY");
        float nhanVatZ = PlayerPrefs.GetFloat("NhanVatZ");
        Vector3 ViTriNhanVat=new Vector3(nhanVatX, nhanVatY, nhanVatZ);
        NhanVatG.transform.position = ViTriNhanVat;

        //Khôi phục lượng máu hiện tại
        int mauNVHienTai = PlayerPrefs.GetInt("MauNVHienTai");
        quanLyThongSoNhanVat.MauHientai = mauNVHienTai;
        thanhMau.CapnhatMau(quanLyThongSoNhanVat.MauHientai, quanLyThongSoNhanVat.MauToiDaNhanVat);
        //Khôi phục thanh hồi chiêu
        float ThoiGianHoiChieuNV = PlayerPrefs.GetFloat("ThoiGianHoiChieuNV");
        float HoiChieuCuonNV = PlayerPrefs.GetFloat("HoiChieuCuonNV");
        NhanVatS.ThoiGianHoiChieu = ThoiGianHoiChieuNV;
        NhanVatS.HoiChieuCuon = HoiChieuCuonNV;
        ThanhKyNangNhanVat thanhKyNangNhanVat=FindObjectOfType<ThanhKyNangNhanVat>();
        thanhKyNangNhanVat.CapNhatHoiChieu(1- NhanVatS.ThoiGianHoiChieu / NhanVatS.HoiChieuCuon);
        //Khôi phục lượng giáp hiện tại
        int giapNVHienTai = PlayerPrefs.GetInt("GiapNVHienTai");
        quanLyThongSoNhanVat.GiapHienTai = giapNVHienTai;
        thanhGiap.CapnhatGiap(quanLyThongSoNhanVat.GiapHienTai, quanLyThongSoNhanVat.GiapToiDa);
        //Khôi phục sát thương của nhân vật
        int satThuongNhoNhat= PlayerPrefs.GetInt("SatThuongNhoNhatNV");
        int satThuongLonNhat = PlayerPrefs.GetInt("SatThuongLonNhatNV");
        quanLyThongSoNhanVat.SatThuongNhoNhat= satThuongNhoNhat;
        quanLyThongSoNhanVat.SatThuongLonNhat=satThuongLonNhat;
        //Khôi phục số kill 
        int soKillNV = PlayerPrefs.GetInt("SoKillNV");
        DemQuaiChet.instance.DemSoLuongQuaiChet = soKillNV;
        DemQuaiChet.instance.CapNhatSoLuongQuaiChet();

        //Khôi phục cấp độ và kinh nghiệm
        int capDoNV = PlayerPrefs.GetInt("CapDoNV");        
        quanLyThongSoNhanVat.CapDoNhanVat = capDoNV;
        int kinhNghiemNV = PlayerPrefs.GetInt("KinhNghiemNV");
        NhanVatS.kinhNghiemNhanVat = kinhNghiemNV;
        NhanVatS.CapNhatUI();

        //Khôi phục thời gian
        DongHo DongHo = FindObjectOfType<DongHo>();
        if (DongHo != null)
        {
            float tGSinhTon = PlayerPrefs.GetFloat("TGSinhTon");
            DongHo.ThoiGianTroiQua = tGSinhTon;
        }
        // Khôi phục trạng thái của các quái
        if (PlayerPrefs.HasKey("CacQuai"))
        {
            string json = PlayerPrefs.GetString("CacQuai");
            EnemyDataList enemyDataList = JsonUtility.FromJson<EnemyDataList>(json);

            foreach (EnemyData data in enemyDataList.enemies)
            {
                Vector3 position = new Vector3(data.posX, data.posY, data.posZ);
                //Tạo quái dựa vào tên quái prefabs đã lưu
                string tenQuaiPrefabs = data.tenQuaiPrefabs.Replace("(Clone)", "").Trim();
                GameObject quaiPrefab = Resources.Load<GameObject>("AllQuai/"+tenQuaiPrefabs);
                if (quaiPrefab != null)
                {
                    GameObject enemy = Instantiate(quaiPrefab, position, Quaternion.identity);
                    enemy.name = tenQuaiPrefabs + "(Clone)"; // Đặt lại tên với (Clone)
                    enemy.GetComponent<QuanLyQuai>().LuongMau = data.mau;
                }
                
            }
        }
        //Khôi phục item rơi
        if (PlayerPrefs.HasKey("CacItem"))
        {
            string json2 = PlayerPrefs.GetString("CacItem");
            ItemDataList itemDataList = JsonUtility.FromJson<ItemDataList>(json2);

            foreach (ItemData data in itemDataList.items)
            {
                Vector3 positem = new Vector3(data.posX, data.posY, data.posZ);
                //Tạo item dựa vào tên item prefabs đã lưu
                string tenItemPrefabs = data.tenItem.Replace("(Clone)", "").Trim();
                GameObject itemPrefab = Resources.Load<GameObject>("Item/" + tenItemPrefabs);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, positem, Quaternion.identity);
                    item.name = tenItemPrefabs + "(Clone)"; // Đặt lại tên với (Clone)

                }
                
            }
        }

        //Khôi phục trạng thái boss
        if (PlayerPrefs.GetInt("CoDuLieuBoss") == 1) //Có dữ liệu boss thì mới khôi phục
        {
            float bossX = PlayerPrefs.GetFloat("BossX");
            float bossY = PlayerPrefs.GetFloat("BossY");
            float bossZ = PlayerPrefs.GetFloat("BossZ");
            Vector3 ViTriBoss = new Vector3(bossX, bossY, bossZ);

            string tenBoss = PlayerPrefs.GetString("TenBoss");
            string tenBossDaChinh = tenBoss.Replace("(Clone)", "").Trim();
            GameObject bossPrefab = Resources.Load<GameObject>("AllQuai/" + tenBossDaChinh);
            if (bossPrefab != null)
            {
                GameObject boss = Instantiate(bossPrefab, ViTriBoss, Quaternion.identity);
                boss.name = tenBossDaChinh + "(Clone)";
                boss.GetComponent<QuanLyBoss>().LuongMau = PlayerPrefs.GetInt("MauBoss");
            }
        }

        //Khôi phục trạng thái điểm buff
        GameObject DiemBuff = GameObject.Find("DiemBuff");
        
        if(DiemBuff!=null)
        {
            if (PlayerPrefs.GetInt("TonTaiDiemBuff") == 0)
            {
                Destroy(DiemBuff.gameObject);
                ConTro.SetActive(false);
            }
        }    
        

    }
    void ResetDuLieu()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Đảm bảo rằng dữ liệu bị xóa ngay lập tức
        Debug.Log("Tất cả dữ liệu PlayerPrefs đã được reset.");
    }

    [System.Serializable]
    private class EnemyDataList
    {
        public List<EnemyData> enemies;

        public EnemyDataList(List<EnemyData> enemies)
        {
            this.enemies = enemies;
        }
    }
    [System.Serializable]
    private class ItemDataList
    {
        public List<ItemData> items;

        public ItemDataList(List<ItemData> items)
        {
            this.items = items;
        }
    }
}
[System.Serializable]
public class EnemyData
{
    public float posX;
    public float posY;
    public float posZ;
    public int mau;
    public string tenQuaiPrefabs;

    public EnemyData(Vector3 position, int mau,string tenQuaiPrefabs)
    {
        this.posX = position.x;
        this.posY = position.y;
        this.posZ = position.z;
        this.mau = mau;
        this.tenQuaiPrefabs = tenQuaiPrefabs;
    }
}
[System.Serializable]
public class ItemData
{
    public float posX;
    public float posY;
    public float posZ;
    public string tenItem;

    public ItemData(Vector3 position, string tenItem)
    {
        this.posX = position.x;
        this.posY = position.y;
        this.posZ = position.z;
        this.tenItem = tenItem;
    }
}
