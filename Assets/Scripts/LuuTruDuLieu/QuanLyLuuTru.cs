using System.IO;
using UnityEngine;

public class QuanLyLuuTru : MonoBehaviour
{
    private static string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/gamedata.json";
    }

    public static void LuuGame(DuLieuGame gameData)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogWarning("Đường dẫn tệp không được khởi tạo.");
            return;
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Dữ liệu game đã được lưu vào " + filePath);
    }

    public static DuLieuGame TaiGame()
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogWarning("Đường dẫn tệp không được khởi tạo.");
            return null;
        }

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DuLieuGame gameData = JsonUtility.FromJson<DuLieuGame>(json);
            Debug.Log("Đang tải dữ liệu game từ " + filePath);
            return gameData;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy dữ liệu game từ " + filePath);
            return null;
        }
    }
}
