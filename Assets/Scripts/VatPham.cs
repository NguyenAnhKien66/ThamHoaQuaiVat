using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatPham : MonoBehaviour
{
    public enum LoaiVatPham
    {
        TangMau,
        TangDame,
        TangGiap
    }

    public LoaiVatPham loaiVatPham;
    public int GiaTri;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NhanVat nhanVat = collision.GetComponent<NhanVat>();
            if (nhanVat != null)
            {
                nhanVat.NhatVatPham(this);
                Destroy(gameObject); 
            }
        }
    }
}
