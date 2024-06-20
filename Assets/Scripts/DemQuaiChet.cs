using TMPro;
using UnityEngine;

public class DemQuaiChet : MonoBehaviour
{
    public static DemQuaiChet instance;
    public TextMeshProUGUI DemQuaiChettxt;
    private int DemSoLuongQuaiChet;

    private void Start()
    {
        CapNhatSoLuongQuaiChet();
    }

    private void Awake()
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

    public void ThemSoluong()
    {
        DemSoLuongQuaiChet++;
        CapNhatSoLuongQuaiChet();
    }

    private void CapNhatSoLuongQuaiChet()
    {
        if (DemQuaiChettxt != null)
        {
            DemQuaiChettxt.text = "Kills: " + DemSoLuongQuaiChet;
        }
    }

    public int LaySoLuongHienTai()
    {
        return DemSoLuongQuaiChet;
    }
}
