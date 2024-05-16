using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChucNang : MonoBehaviour
{
    int Map = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void ChonMap1()
    {
        Map = 1;
    }
    public void ChonMap2()
    {
        Map = 2;
    }
    public void ChonMap3()
    {
        Map = 3;
    }
    public void ChonMap4()
    {
        Map = 4;
    }
}
