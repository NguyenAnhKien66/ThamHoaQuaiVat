using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLSinhBoss : MonoBehaviour
{
    public GameObject quaiBossPf;
    public Transform[] viTriSinhRa;
    public float thoiGianXuatHien;
   
    void Start()
    {
        StartCoroutine(SinhBoss());
    }
    IEnumerator SinhBoss()
    {
        yield return new WaitForSeconds(thoiGianXuatHien);
       
        if(quaiBossPf != null)
        {
            Vector3 ViTri = new Vector3(viTriSinhRa[0].position.x, viTriSinhRa[0].position.y,0f);
            Instantiate(quaiBossPf, ViTri, Quaternion.identity);               
        }   
    }
}
