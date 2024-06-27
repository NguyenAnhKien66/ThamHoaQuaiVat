using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyPhatSinhQuai : MonoBehaviour
{
    public GameObject quaiPrefab;
    public Transform[] viTriSinhRa;
    public float thoiGianBatDau = 2f;
    public float thoiGianLapLai = 30f;
    private int soLuongQuai = 1;
    public float khoangCachMinGiuaCacQuai = 2f; // Khoang cach toi thieu giua cac quai

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
                    Transform viTriNgauNhien = viTriSinhRa[Random.Range(0, viTriSinhRa.Length)];

                    // Kiem tra vi tri hop le truoc khi sinh quai vat
                    if (KiemTraViTriHopLe(viTriNgauNhien.position))
                    {
                        // Tạo một Vector3 mới với giá trị Z là 0
                        Vector3 viTriMoi = new Vector3(viTriNgauNhien.position.x, viTriNgauNhien.position.y, 0f);

                        Instantiate(quaiPrefab, viTriMoi, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogWarning("Không thể sinh ra quái vật tại vị trí này do quái vật đã có.");
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab quái vật đã bị hủy. Không thể sinh ra quái vật.");
                }

                yield return new WaitForSeconds(1f); // Đợi 0.1 giây giữa mỗi lần sinh quái
            }
            soLuongQuai++;

            yield return new WaitForSeconds(thoiGianLapLai);
        }
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
