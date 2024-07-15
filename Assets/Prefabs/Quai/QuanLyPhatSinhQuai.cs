using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class QuanLyPhatSinhQuai : MonoBehaviour
{
    public GameObject[] quaiPrefabs; 
    public Transform[] viTriSinhRa; 
    public float thoiGianBatDau = 2f;
    public float thoiGianLapLai = 60f; 
    public float khoangCachMinGiuaCacQuai = 2f; 
    public int soLuongQuaiYeuCau = 3; 
    public float ThoiGianTangMau = 60f;
    public int soLuongQuaiDot1 = 1; 
    public int soLuongQuaiDot2 = 1;
    public int soLuongQuaiDot3 = 1;
    public int soLuongQuaiDot4 = 1;
    public int soLuongQuaiDot5 = 1;
    public int soLuongQuaiDot6 = 1;
    public int soluong = 0;
    public int soluongchet = 0;
    private int[] soLuongQuaiChoMoiDot; 
    public Dictionary<Transform, int> soLuongQuaiDaPhatSinh; 
    private float thoiGianDaTroiQua = 0f;
    public int gioiHanQuai=0;

    private void Start()
    {
        soLuongQuaiChoMoiDot = new int[] { soLuongQuaiDot1, soLuongQuaiDot2, soLuongQuaiDot3, soLuongQuaiDot4, soLuongQuaiDot5, soLuongQuaiDot6 };
        soLuongQuaiDaPhatSinh = new Dictionary<Transform, int>();

        foreach (Transform viTri in viTriSinhRa)
        {
            soLuongQuaiDaPhatSinh[viTri] = 0;
        }

        StartCoroutine(SinhQuaiDinhKy());
    }

    IEnumerator SinhQuaiDinhKy()
    {
        yield return new WaitForSeconds(thoiGianBatDau);

        while (true)
        {
            thoiGianDaTroiQua += thoiGianLapLai;

            int indexDotSinh = Mathf.FloorToInt(thoiGianDaTroiQua / 60f);
            if (indexDotSinh < soLuongQuaiChoMoiDot.Length)
            {
                int soluongCanSinh = soLuongQuaiChoMoiDot[indexDotSinh];

                for (int i = 0; i < soluongCanSinh; i++)
                {
                    if (quaiPrefabs.Length > 0)
                    {
                        Transform viTriNgauNhien = viTriSinhRa[Random.Range(0, viTriSinhRa.Length)];

                        if (KiemTraViTriHopLe(viTriNgauNhien.position))
                        {
                            if (soLuongQuaiDaPhatSinh[viTriNgauNhien] < gioiHanQuai)
                            {
                                
                                GameObject quaiPrefab = quaiPrefabs[Random.Range(0, quaiPrefabs.Length)];
                                Vector3 viTriMoi = new Vector3(viTriNgauNhien.position.x, viTriNgauNhien.position.y, 0f);
                                GameObject quaiInstance = Instantiate(quaiPrefab, viTriMoi, Quaternion.identity);

                                QuanLyQuai quanLyQuai = quaiInstance.GetComponent<QuanLyQuai>();
                                if (quanLyQuai != null)
                                {
                                    float thoiGianDaTroiQuaQuai = Time.timeSinceLevelLoad;
                                    quanLyQuai.LuongMau = 20 + ((int)(thoiGianDaTroiQuaQuai / ThoiGianTangMau) * 20);
                                    quanLyQuai.MauHienTai = quanLyQuai.LuongMau;

                                    if (quanLyQuai.MauHienTai > quanLyQuai.MauToiDa)
                                    {
                                        quanLyQuai.MauHienTai = quanLyQuai.MauToiDa;
                                    }
                                }

                                soLuongQuaiDaPhatSinh[viTriNgauNhien]++;
                                soluong++;
                                Debug.Log("Spawned " + soLuongQuaiDaPhatSinh[viTriNgauNhien] + " monsters from this portal.");
                            }
                            else
                            {
                                Debug.LogWarning("Reached the limit of monsters from this portal (60).");

                                /* if (soluong < 20)
                                 {
                                     StartCoroutine(TaiSinh());
                                 }*/
                                if(soLuongQuaiDaPhatSinh[viTriNgauNhien]==60)
                                {
                                    soLuongQuaiDaPhatSinh[viTriNgauNhien] -= soluongchet;
                                    soluongchet = 0;
                                }

                            }
                        }
                        else
                        {
                            Debug.LogWarning("Cannot spawn monster at this location due to proximity.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No monster prefabs in the array. Cannot spawn monsters.");
                    }

                    yield return new WaitForSeconds(1f); 
                }
            }

            yield return new WaitForSeconds(thoiGianLapLai); 
        }
    }

    IEnumerator TaiSinh()
    {
        yield return new WaitForSeconds(1f); 

        foreach (Transform viTri in viTriSinhRa)
        {
            if (soLuongQuaiDaPhatSinh[viTri] < 20)
            {
                foreach (GameObject quai in GameObject.FindGameObjectsWithTag("QuaiVat"))
                {
                    if (!quai.activeInHierarchy)
                    {
                        quai.transform.position = viTri.position;
                        quai.SetActive(true);
                        soLuongQuaiDaPhatSinh[viTri]++;  
                        break;
                    }
                }
            }
        }
    }

    public void demquai()
    {
        soluongchet++;
        /*StartCoroutine(TaiSinh());*/ 
    }

    bool KiemTraViTriHopLe(Vector3 viTri)
    {
        foreach (var quai in GameObject.FindGameObjectsWithTag("QuaiVat"))
        {
            if (Vector3.Distance(quai.transform.position, viTri) < khoangCachMinGiuaCacQuai)
            {
                return false;
            }
        }
        return true;
    }
}
