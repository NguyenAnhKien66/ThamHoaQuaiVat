using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyPhatSinhQuai : MonoBehaviour
{
    public GameObject quaiPrefab;
    public Transform[] viTriSinhRa;
    public float thoiGianBatDau = 2f;
    public float thoiGianLapLai = 5f;
    private int soLuongQuai = 1;

    GameObject obj;
    bool ThoatKhoiWhile = false;
    private void Start()
    {
        StartCoroutine(SinhQuaiDinhKy());
        Debug.Log("ViTriSinhRa = " + viTriSinhRa[0]);
    }

    IEnumerator SinhQuaiDinhKy()
    {
        yield return new WaitForSeconds(thoiGianBatDau);

        while (true)
        {
            for (int i = 0; i < soLuongQuai; i++)
            {
                if (quaiPrefab != null)
                { 
                    obj=GameObject.Find("Boss(Clone)");
                    if(obj==null)
                    {
                        Transform viTriNgauNhien = viTriSinhRa[Random.Range(0, viTriSinhRa.Length)];

                        // Tạo một Vector3 mới với giá trị Z là 0
                        Vector3 viTriMoi = new Vector3(viTriNgauNhien.position.x, viTriNgauNhien.position.y, 0f);

                        Instantiate(quaiPrefab, viTriMoi, Quaternion.identity);
                    }    
                    else
                    {
                        ThoatKhoiWhile = true;
                        break;

                    }    
                    
                }
                else
                {
                    Debug.LogWarning("Prefab quái vật đã bị hủy. Không thể sinh ra quái vật.");
                }
            }
            if(ThoatKhoiWhile)
            {
                break;
            }

            soLuongQuai++;

            yield return new WaitForSeconds(thoiGianLapLai);
        }
    }
}
