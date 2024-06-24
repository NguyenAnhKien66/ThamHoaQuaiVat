using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class QuanLyVatPham : MonoBehaviour
{
    public GameObject VatPhamMau;
    public GameObject VatPhamGiap;
    public GameObject VatPhamDame;
    public float tileroi = 0.5f;

    public void RoiVatPham(Vector3 vitri)
    {
        if (Random.value < tileroi)
        {
            int loaiVatPham = Random.Range(0, 3);
            GameObject VatPhamSeRoi = null;
            VatPham.LoaiVatPham VatPhamRoi = VatPham.LoaiVatPham.TangMau;
            int GiaTri = 0;
            switch (loaiVatPham)
            {
                case 0:
                    VatPhamSeRoi = VatPhamMau;
                    VatPhamRoi = VatPham.LoaiVatPham.TangMau;
                    GiaTri = 10;
                    break;
                case 1:
                    VatPhamSeRoi = VatPhamDame;
                    VatPhamRoi = VatPham.LoaiVatPham.TangDame;
                    GiaTri = 5;
                    break;
                case 2:
                    VatPhamSeRoi = VatPhamGiap;
                    VatPhamRoi = VatPham.LoaiVatPham.TangGiap;
                    GiaTri = 10;
                    break;
            }
            if (VatPhamSeRoi != null)
            {
                GameObject VatPhamObj = Instantiate(VatPhamSeRoi, vitri, Quaternion.identity);
                VatPham vatPham = VatPhamObj.GetComponent<VatPham>();
                vatPham.loaiVatPham = VatPhamRoi;
                vatPham.GiaTri = GiaTri;

                // hien thi tren ban do
                SpriteRenderer spriteRenderer = VatPhamObj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
            }
        }
    }
}
