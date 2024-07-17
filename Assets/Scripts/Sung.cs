using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sung : MonoBehaviour
{
    public QuanLyThongSoNhanVat quanLyThongSoNhanVat;
    public GameObject VienDan; // Doi tuong vien dan
    public Transform ViTriBan; // Vi tri ban dan
    public GameObject TiaLua; // Hieu ung tia lua
    private float tocdoban; // Bien dem de theo doi thoi gian giua cac lan ban
    // Thong so sung
    public float TocDoban = 2f;
    public float TocDobanGiamKhiLenCap = 0.1f;// Toc do ban
    public float LucBan = 10f; // Luc ban
    public AudioClip AmThanhDan; // am thanh dan
    private AudioSource NguonAmThanh;
    public bool GapDoiDan=false;
    public int DieuKienGapDoiDan = 5;
    private void Start()
    {
        NguonAmThanh = GetComponent<AudioSource>();
    }
    void Update()
    {
        XoaySung(); // Goi ham xoay sung theo vi tri chuot
        tocdoban -= Time.deltaTime; // Giam tocdoban theo thoi gian
        if (Input.GetMouseButton(0) && tocdoban < 0) // Kiem tra neu nut chuot trai duoc nhan va tocdoban < 0
        {
            if(quanLyThongSoNhanVat.CapDoNhanVat>=5 && GapDoiDan==true)
            {
                StartCoroutine(BanDanHaiLan());
            }
            else
            {
                BanDan(); // Goi ham ban dan
            }
            
        }
    }

    IEnumerator BanDanHaiLan()
    {
        BanDan(); 
        yield return new WaitForSeconds(0.5f); 
        BanDan(); 
        yield return new WaitForSeconds(1f);
    }

    void XoaySung()
    {
        // Chuyen toa do tu pixel sang toa do the gioi
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position; // Huong nhin cua sung
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // Tinh goc xoay

        Quaternion rotation1 = Quaternion.Euler(0, 0, angle); // Tao quaternion tu goc xoay
        transform.rotation = rotation1; // Xoay sung theo goc tinh duoc

        // Dieu chinh scale de sung huong len tren hay xuong duoi
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(0.5f, -0.5f, 0);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0);
        }
    }

    void BanDan()
    {
        tocdoban = TocDoban; // Reset tocdoban ve gia tri mac dinh
        GameObject bulletTmp = Instantiate(VienDan, ViTriBan.position, Quaternion.identity); // Tao vien dan moi
        // Hieu ung tia lua sau moi lan ban
        Instantiate(TiaLua, ViTriBan.position, transform.rotation, transform);

        if (AmThanhDan != null)
        {
            NguonAmThanh.PlayOneShot(AmThanhDan);
        }

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>(); // Lay thanh phan Rigidbody2D cua vien dan
        rb.AddForce(transform.right * LucBan, ForceMode2D.Impulse); // Ap luc len vien dan de ban no
    }
    public void CapNhatTocDoDan()
    {
        if(TocDoban>=0.5f)
        {
            TocDoban -= TocDobanGiamKhiLenCap;
        }
        else
        {
            TocDoban = 0.5f;
        }
        
        Debug.Log("da cap nhat toc do sung");
    }
}
