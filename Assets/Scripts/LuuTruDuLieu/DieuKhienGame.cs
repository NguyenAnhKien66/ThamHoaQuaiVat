using UnityEngine;
using UnityEngine.SceneManagement;

public class DieuKhienGame : MonoBehaviour
{
    public static DieuKhienGame instance;
    public DuLieuGame duLieuGame;
    private const int soMap = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Tải dữ liệu game khi bắt đầu
        duLieuGame = QuanLyLuuTru.TaiGame();
        if (duLieuGame == null)
        {
            duLieuGame = new DuLieuGame(soMap);
        }

        // Debugging
        Debug.Log("duLieuGame đã được khởi tạo.");
        if (duLieuGame.maps != null)
        {
            Debug.Log("Số lượng maps trong duLieuGame: " + duLieuGame.maps.Length);
        }

        // Lưu dữ liệu game định kỳ sau mỗi 30 giây
        InvokeRepeating("LuuDuLieu", 30f, 30f);
    }

    void LuuDuLieu()
    {
        // Đảm bảo rằng duLieuGame và maps không bị null trước khi truy cập
        if (duLieuGame != null && duLieuGame.maps != null && duLieuGame.maps.Length > 0)
        {
            // Cập nhật dữ liệu game dựa trên gameplay (ví dụ: cập nhật điểm số, thời gian, số lượng kills)

            // Lưu dữ liệu game
            QuanLyLuuTru.LuuGame(duLieuGame);
            Debug.Log("Dữ liệu game đã được lưu tự động.");
        }
        else
        {
            Debug.LogWarning("duLieuGame hoặc maps là null.");
        }
    }

    // Tùy chọn: Phương thức để đặt lại dữ liệu hoặc thực hiện các hành động khác
    public void DatLaiDuLieuGame()
    {
        duLieuGame = new DuLieuGame(soMap);
        QuanLyLuuTru.LuuGame(duLieuGame);
        Debug.Log("Dữ liệu game đã được đặt lại.");
    }
}
