using System.Collections;
using System.Collections.Generic;
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
        if(HienHopThoaiLuu)
        {
            HopThoaiLuu.SetActive(true);
            HienHopThoaiLuu = false;
        }    
    }
    void OnApplicationQuit()
    {
        if(!ChoThoat)
        {
            HienHopThoaiLuu=true;
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
        //Lưu màn hình map
        int ViTriManHinh=SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("ViTriManHinh", ViTriManHinh);
        //Lưu vị trí của người chơi
        Vector3 ViTriNhanVat = NhanVatG.transform.position;
        PlayerPrefs.SetFloat("NhanVatX", ViTriNhanVat.x);
        PlayerPrefs.SetFloat("NhanVatY", ViTriNhanVat.y);
        PlayerPrefs.SetFloat("NhanVatZ", ViTriNhanVat.z);
        //Lưu lượng máu hiện tại
        int MauHienTai = quanLyThongSoNhanVat.MauHientai;
        PlayerPrefs.SetInt("MauNVHienTai", MauHienTai);
        //Lưu lượng mana hiện tại
        float ThoiGianHoiChieu =NhanVatS.ThoiGianHoiChieu;
        float HoiChieuCuon = NhanVatS.HoiChieuCuon;
        PlayerPrefs.SetFloat("ThoiGianHoiChieuNV", ThoiGianHoiChieu);
        PlayerPrefs.SetFloat("HoiChieuCuonNV", HoiChieuCuon);
        //Lưu lượng giáp hiện tại
        int GiapHienTai = quanLyThongSoNhanVat.GiapHienTai;
        PlayerPrefs.SetInt("GiapNVHienTai", GiapHienTai);
        //Lưu số kill
        if(DemQuaiChet.instance != null)
        {
            int SoKill=DemQuaiChet.instance.LaySoLuongHienTai();
            PlayerPrefs.SetInt("SoKillNV", SoKill);
        }
        //Lưu cấp độ và kinh nghiệm
        int CapDo = quanLyThongSoNhanVat.CapDoNhanVat;
        int KinhNghiem = NhanVatS.kinhNghiemNhanVat;
        PlayerPrefs.SetInt("CapDoNV", CapDo);
        PlayerPrefs.SetInt("KinhNghiemNV", KinhNghiem);
        //Lưu thời gian
        DongHo DongHo = FindObjectOfType<DongHo>();
        if( DongHo != null )
        {
            float TGSinhTon = DongHo.GetThoiGianTroiQua();
            PlayerPrefs.SetFloat("TGSinhTon", TGSinhTon);
        }

        // Lưu trạng thái của các quái
        List<EnemyData> enemyDataList = new List<EnemyData>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("QuaiVat"))
        {
            Vector3 position = enemy.transform.position;
            int mau = enemy.GetComponent<QuanLyQuai>().LuongMau;
            string tenQuaiPrefabs = GetPrefabNameWithoutExtension(enemy.name);
            enemyDataList.Add(new EnemyData(position, mau, tenQuaiPrefabs)); //Lưu vị trí, máu và tên của từng quái vào danh sách
        }

        string json = JsonUtility.ToJson(new EnemyDataList(enemyDataList)); //add danh sách dữ liệu các quái vào file json
        PlayerPrefs.SetString("CacQuai", json);

        // Lưu trạng thái của boss
        GameObject BossG= GameObject.FindWithTag("Boss");
        QuanLyBoss BossS = FindObjectOfType<QuanLyBoss>();
        if(BossG != null && BossS!=null)
        {
            //Lưu vị trí boss
            Vector3 position= BossG.transform.position;
            PlayerPrefs.SetFloat("BossX", position.x);
            PlayerPrefs.SetFloat("BossY", position.y);
            PlayerPrefs.SetFloat("BossZ", position.z);
            //Lưu máu boss
            int mauBoss = BossS.LuongMau;
            PlayerPrefs.SetInt("MauBoss", mauBoss);
            //Lưu tên boss 
            string tenBossPrefabs= GetPrefabNameWithoutExtension(BossG.name);
            PlayerPrefs.SetString("TenBoss",tenBossPrefabs);
            PlayerPrefs.SetInt("CoDuLieuBoss", 1); //Có dữ liệu
        }
        else
        {
            PlayerPrefs.SetInt("CoDuLieuBoss", 0); //Không có dữ liệu
        }
        // Đánh dấu rằng game đã được lưu
        PlayerPrefs.SetInt("DaLuuGame", 1);

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
    [System.Serializable]
    private class EnemyDataList
    {
        public List<EnemyData> enemies;

        public EnemyDataList(List<EnemyData> enemies)
        {
            this.enemies = enemies;
        }
    }
}
