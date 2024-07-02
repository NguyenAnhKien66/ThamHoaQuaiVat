using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuanLyPhatSinhQuai : MonoBehaviour
{
    public GameObject[] quaiPrefabs; // Array of different monster prefabs
    public Transform[] viTriSinhRa;
    public float thoiGianBatDau = 2f;
    public float thoiGianLapLai = 30f;
    private int soLuongQuai = 1;
    public float khoangCachMinGiuaCacQuai = 2f; // Minimum distance between monsters

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
                if (quaiPrefabs.Length > 0)
                {
                    Transform viTriNgauNhien = viTriSinhRa[Random.Range(0, viTriSinhRa.Length)];

                    // Check for valid spawn position before spawning the monster
                    if (KiemTraViTriHopLe(viTriNgauNhien.position))
                    {
                        // Select a random monster prefab
                        GameObject quaiPrefab = quaiPrefabs[Random.Range(0, quaiPrefabs.Length)];

                        // Create a new Vector3 with Z value as 0
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
                    Debug.LogWarning("Không có prefab quái vật nào trong mảng. Không thể sinh ra quái vật.");
                }

                yield return new WaitForSeconds(1f); // Wait 1 second between each monster spawn
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
