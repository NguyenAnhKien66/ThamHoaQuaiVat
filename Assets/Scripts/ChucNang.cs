using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChucNang : MonoBehaviour
{
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
}
