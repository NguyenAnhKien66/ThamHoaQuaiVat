using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChucNangMenuPause : MonoBehaviour
{
    public GameObject MenuTamDung;
    public GameObject HopThoaiLuu;

    void Start()
    {
        if (MenuTamDung == null)
        {
            Debug.LogError("MenuTamDung is not assigned.");
        }
    }
    public void TamDung()
    {
        if (HopThoaiLuu.activeSelf == false)
        {
            MenuTamDung.SetActive(true);
            Time.timeScale = 0.0001f;
        }
    }
    public void ThoatGamePlay()
    {
        XoaFileLuu();
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
    public void BatAm()
    {
        AmThanh.bAmThanh = false;
    }
    public void TatAm()
    {
        AmThanh.bAmThanh = true;
        Debug.Log("Da tat");
    }
    public void XoaFileLuu()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File lưu đã được xóa!");
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
