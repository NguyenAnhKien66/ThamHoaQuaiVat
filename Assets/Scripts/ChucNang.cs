using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChucNang : MonoBehaviour
{
    int ChonMap = 1; //map được chọn    
    public Button nut_batdau;
    // Start is called before the first frame update
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
        SceneManager.LoadScene(1);
    }
    public void ThoatGame()
    {
        Application.Quit();
    }
    public void TroVeMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Map1()
    {
        ChonMap = 1;
    }
    public void Map2()
    {
        ChonMap = 2;
    }
    public void Map3()
    {
        ChonMap = 3;
    }
    public void Map4()
    {
        ChonMap = 4;
    }
    public void Choi()
    {
        if (ChonMap == 1)
        {
            //Vào màn hình chơi game của map 1
        }
        else if (ChonMap == 2)
        {
            //Vào màn hình chơi game của map 2
        }
        else if (ChonMap == 3)
        {
            //Vào màn hình chơi game của map 3
        }
        else if (ChonMap == 4)
        {
            //Vào màn hình chơi game của map 4
        }

    }
    public void Xong()
    {
        SceneManager.LoadScene(1);
    }
}
