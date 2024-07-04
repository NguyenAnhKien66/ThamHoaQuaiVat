using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChucNangMenuPause : MonoBehaviour
{
    [SerializeField] GameObject MenuTamDung;
    void Start()
    {
        if (MenuTamDung == null)
        {
            Debug.LogError("MenuTamDung is not assigned.");
        }
    }
    public void TamDung()
    {
        MenuTamDung.SetActive(true);
        Time.timeScale = 0.0001f;
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
