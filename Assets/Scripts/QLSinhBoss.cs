using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLSinhBoss : MonoBehaviour
{
    public GameObject quaiBossPf;
    public Transform[] viTriSinhRa;
    public float thoiGianXuatHien;
    public AudioClip AmThanh; // am thanh 
    private AudioSource NguonAmThanh;
    float ThoiGianTroiQua;
    void Start()
    {
        NguonAmThanh = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("TiepTuc") == 1)
        {
            ThoiGianTroiQua = PlayerPrefs.GetFloat("TGSinhTon"); ;
        }
        else
        {
            ThoiGianTroiQua = 0f;
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
