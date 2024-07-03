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
    void Start()
    {
        NguonAmThanh = GetComponent<AudioSource>();
        StartCoroutine(SinhBoss());
    }
    IEnumerator SinhBoss()
    {
        float thoiGianCanhBao = 5f; 
        yield return new WaitForSeconds(thoiGianXuatHien - thoiGianCanhBao);

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
