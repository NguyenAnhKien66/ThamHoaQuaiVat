using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiaDiem : MonoBehaviour
{
    public string TenDiaDiem;
    public TextMeshProUGUI txtDiaDiem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(txtDiaDiem != null)
            {
                txtDiaDiem.text = TenDiaDiem;
                txtDiaDiem.gameObject.SetActive(true);
            }      
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.CompareTag("Player"))
        {
            if(txtDiaDiem != null)
                txtDiaDiem.gameObject.SetActive(false);
        }
    }
}
