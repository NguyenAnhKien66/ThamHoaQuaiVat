using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static QLLuuQuaTrinh;

public class QLSinhBoss : MonoBehaviour
{
    public GameObject quaiBossPf;
    public Transform[] viTriSinhRa;
    public float thoiGianXuatHien;
    public AudioClip AmThanh; // am thanh 
    private AudioSource NguonAmThanh;
    float ThoiGianTroiQua=0f;
    void Start()
    {
        NguonAmThanh = GetComponent<AudioSource>();
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            if (PlayerPrefs.GetInt("TiepTuc") == 1)
            {
                ThoiGianTroiQua = gameData.TGSinhTon;
            }
            else
            {
                ThoiGianTroiQua = 0f;
            }
        }
        if (PlayerPrefs.GetInt("TiepTuc")==0 || PlayerPrefs.GetInt("CoDuLieuBoss") == 0)
        {
            StartCoroutine(SinhBoss());
        }
        
        
    }
    IEnumerator SinhBoss()
    {
        float thoiGianCanhBao = 5f; 
        yield return new WaitForSeconds(thoiGianXuatHien - thoiGianCanhBao- ThoiGianTroiQua);

        if (AmThanh != null)
        {
            NguonAmThanh.PlayOneShot(AmThanh);
        }

        yield return new WaitForSeconds(thoiGianCanhBao);

        
        if (quaiBossPf != null)
        {
            Vector3 ViTri = new Vector3(viTriSinhRa[0].position.x, viTriSinhRa[0].position.y, 0f);
            Instantiate(quaiBossPf, ViTri, Quaternion.identity);
        }
    }
}
