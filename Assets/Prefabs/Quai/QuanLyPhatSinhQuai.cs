using System.Collections;
using UnityEngine;
using System.Collections.Generic;

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

    private int[] soLuongQuaiChoMoiDot; // Mảng lưu số lượng quái cần sinh cho mỗi đợt
    public Dictionary<Transform, int> soLuongQuaiDaPhatSinh; // Dictionary lưu số lượng quái đã sinh từng cổng
    public int soluong;
    private float thoiGianDaTroiQua = 0f; // Thời gian đã trôi qua

    private void Start()
    {
        // Khởi tạo mảng lưu trữ số lượng quái vật cho từng đợt và số lượng đã sinh ra
        soLuongQuaiChoMoiDot = new int[] { soLuongQuaiDot1, soLuongQuaiDot2, soLuongQuaiDot3, soLuongQuaiDot4, soLuongQuaiDot5, soLuongQuaiDot6 };
        soLuongQuaiDaPhatSinh = new Dictionary<Transform, int>();

        // Khởi tạo số lượng đã sinh cho từng cổng
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

            // Lấy số lượng quái cần sinh cho đợt hiện tại dựa vào thời gian đã trôi qua
            int indexDotSinh = Mathf.FloorToInt(thoiGianDaTroiQua / 60f);
            if (indexDotSinh < soLuongQuaiChoMoiDot.Length)
            {
                int soluongCanSinh = soLuongQuaiChoMoiDot[indexDotSinh];

                // Kiểm tra số lượng quái đã sinh ra từ cổng tương ứng và sinh quái
                for (int i = 0; i < soluongCanSinh; i++)
                {
                    if (quaiPrefabs.Length > 0)
                    {
                        Transform viTriNgauNhien = viTriSinhRa[Random.Range(0, viTriSinhRa.Length)];

                        // Kiểm tra vị trí sinh ra có hợp lệ không
                        if (KiemTraViTriHopLe(viTriNgauNhien.position))
                        {
                            // Kiểm tra số lượng đã sinh từ cổng này có vượt quá giới hạn không
                            if (soLuongQuaiDaPhatSinh[viTriNgauNhien] < 60)
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

                                // Đã sinh ra một quái vật từ cổng này, cập nhật số lượng đã sinh
                                soLuongQuaiDaPhatSinh[viTriNgauNhien]++;
                                soluong = soLuongQuaiDaPhatSinh[viTriNgauNhien];
                                Debug.Log("Đã sinh ra " + soLuongQuaiDaPhatSinh[viTriNgauNhien] + " quái từ cổng này.");
                            }
                            else
                            {
                                Debug.LogWarning("Đã đạt giới hạn số lượng quái từ cổng này (80 quái).");
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
            }
            else
            {
                thoiGianLapLai = 20f;
                // Đã đạt yêu cầu số lượng quái vật, có thể làm gì đó khi đạt điều kiện
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
