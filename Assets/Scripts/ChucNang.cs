using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChucNang : MonoBehaviour
{
    int ChonMap = 1; //map được chọn    
    public Button nut_batdau;
    [SerializeField] GameObject MenuTamDung;
    [SerializeField] GameObject QLKhoaMap2;
    [SerializeField] GameObject QLKhoaMap3;
    [SerializeField] GameObject QLKhoaMap4;
    [SerializeField] GameObject QLKhoaMap5;
    public TextMeshProUGUI MapChonTxt;
    void Start()
    {
        if (nut_batdau != null)
        {
            nut_batdau.onClick.AddListener(BatDau);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.KeypadEnter)) 
        {
            nut_batdau.onClick.Invoke();
        }
    }
    public void BatDau()
    {
        SceneManager.LoadScene("GameMap");
    }
    public void ThoatGame()
    {
        Application.Quit();
    }
    public void TroVeMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Map1()
    {
        ChonMap = 1;
        MapChonTxt.text = "Map "+ChonMap;
    }
    public void Map2()
    {
        //Nếu Map không hiện bảng khóa mới được ghi nhận sự lựa chọn 
        if (QLKhoaMap2.activeSelf == false)
        {
            ChonMap = 2;
            MapChonTxt.text = "Map " + ChonMap;
        }
    }
    public void Map3()
    {
        //Nếu Map không hiện bảng khóa mới được ghi nhận sự lựa chọn 
        if (QLKhoaMap3.activeSelf == false)
        {
            ChonMap = 3;
            MapChonTxt.text = "Map " + ChonMap;
        }
    }
    public void Map4()
    {
        //Nếu Map không hiện bảng khóa mới được ghi nhận sự lựa chọn 
        if (QLKhoaMap4.activeSelf == false)
        {
            ChonMap = 4;
            MapChonTxt.text = "Map " + ChonMap;
        }
    }
    public void Map5()
    {
        //Nếu Map không hiện bảng khóa mới được ghi nhận sự lựa chọn 
        if (QLKhoaMap5.activeSelf == false)
        {
            ChonMap = 5;
            MapChonTxt.text = "Map " + ChonMap;
        }
    }
    public void Choi()
    {
        if (ChonMap == 1)
        {
            //Vào màn hình chơi game của map 1
            SceneManager.LoadScene("Gameplay1");
        }
        else if (ChonMap == 2)
        {
            //Vào màn hình chơi game của map 2
            SceneManager.LoadScene("Gameplay2");
        }
        else if (ChonMap == 3)
        {
            //Vào màn hình chơi game của map 3
            SceneManager.LoadScene("Gameplay3");
        }
        else if (ChonMap == 4)
        {
            //Vào màn hình chơi game của map 4
            SceneManager.LoadScene("Gameplay4");
        }
        else if (ChonMap == 5)
        {
            //Vào màn hình chơi game của map 5
            SceneManager.LoadScene("Gameplay5");
        }

    }
    public void Xong()
    {
        SceneManager.LoadScene("GameMap"); 
    }
    //Chuc nang pause
    public void TamDung()
    {
        MenuTamDung.SetActive(true);
        Time.timeScale = 0;
    }
    public void ThoatGamePlay()
    {
        SceneManager.LoadScene("GameMap");
        Time.timeScale = 1;
    }
    public void TiepTuc()
    {
        MenuTamDung.SetActive(false);
        Time.timeScale = 1;
    }   
    public void BatDauLai()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
