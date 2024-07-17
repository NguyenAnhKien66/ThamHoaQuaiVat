using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{
    public GameObject levelUp;
    void Start()
    {
        if (levelUp != null)
        {
            levelUp.SetActive(false);
        }
    }

    public void HienThi()
    {
        if (levelUp != null)
        {
            StartCoroutine(HienThiLenManHinh());
        }
    }
    private IEnumerator HienThiLenManHinh()
    {
        levelUp.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelUp.SetActive(false);
    }
}
