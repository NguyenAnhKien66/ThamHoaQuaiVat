using System.Collections;
using UnityEngine;

public class QuanLyPhatSinhQuai : MonoBehaviour
{
    public GameObject[] quaiPrefabs; // Mảng các prefab quái vật
    public Transform[] viTriSinhRa; // Các vị trí sinh ra quái vật
    public float thoiGianBatDau = 2f; // Thời gian chờ trước khi bắt đầu sinh quái vật
    public float thoiGianLapLai = 60f; // Khoảng thời gian giữa các lần sinh quái vật
    public float khoangCachMinGiuaCacQuai = 2f; // Khoảng cách tối thiểu giữa các quái vật
    public int soLuongQuaiYeuCau = 3; // Số lượng quái vật yêu cầu sau 7 phút
    public float ThoiGianTangMau = 60f;
    public int soLuongQuaiDot1 = 1; // Số lượng quái vật ban đầu được sinh ra
    public int soLuongQuaiDot2 = 1;
    public int soLuongQuaiDot3 = 1;
    public int soLuongQuaiDot4 = 1;
    public int soLuongQuaiDot5 = 1;
    public int soLuongQuaiDot6 = 1;
    private int SoluongPhatSinh;
    private float thoiGianDaTroiQua = 0f; // Thời gian đã trôi qua

    private void Start()
    {
        StartCoroutine(SinhQuaiDinhKy());
    }

    IEnumerator SinhQuaiDinhKy()
    {
        yield return new WaitForSeconds(thoiGianBatDau);

        while (true)
        {
            thoiGianDaTroiQua += thoiGianLapLai;

            if (thoiGianDaTroiQua > 60f && thoiGianDaTroiQua < 120f)
            {
                SoluongPhatSinh = soLuongQuaiDot1;
            }
            else if (thoiGianDaTroiQua < 180f)
            {
                SoluongPhatSinh = soLuongQuaiDot2;
            }
            else if (thoiGianDaTroiQua < 240f)
            {
                SoluongPhatSinh = soLuongQuaiDot3;
            }
            else if (thoiGianDaTroiQua < 300f)
            {
                SoluongPhatSinh = soLuongQuaiDot4;
            }
            else if (thoiGianDaTroiQua < 360f)
            {
                SoluongPhatSinh = soLuongQuaiDot5;
            }
            else if (thoiGianDaTroiQua < 420f)
            {
                SoluongPhatSinh = soLuongQuaiDot6;
            }
            else
            {
                thoiGianLapLai = 20f;
                SoluongPhatSinh = soLuongQuaiYeuCau;
            }

            // Sinh ra các quái vật
            for (int i = 0; i < SoluongPhatSinh; i++)
            {
                if (quaiPrefabs.Length > 0)
                {
                    Transform viTriNgauNhien = viTriSinhRa[Random.Range(0, viTriSinhRa.Length)];

                    // Kiểm tra vị trí sinh ra có hợp lệ không
                    if (KiemTraViTriHopLe(viTriNgauNhien.position))
                    {
                        GameObject quaiPrefab = quaiPrefabs[Random.Range(0, quaiPrefabs.Length)];
                        Vector3 viTriMoi = new Vector3(viTriNgauNhien.position.x, viTriNgauNhien.position.y, 0f);

                        GameObject quaiInstance = Instantiate(quaiPrefab, viTriMoi, Quaternion.identity);

                        // Tính toán lượng máu dựa trên thời gian đã trôi qua
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

                            Debug.Log("Quái mới sinh ra với lượng máu: " + quanLyQuai.MauHienTai);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Không thể sinh ra quái vật tại vị trí này do quái vật đã có quá gần.");
                    }
                }
                else
                {
                    Debug.LogWarning("Không có prefab quái vật nào trong mảng. Không thể sinh ra quái vật.");
                }

                yield return new WaitForSeconds(1f); // Chờ 1 giây giữa mỗi lần sinh quái vật
            }

            // Chờ đợi khoảng thời gian lặp lại
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
