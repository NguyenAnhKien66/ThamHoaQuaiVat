using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaiBayAI : MonoBehaviour
{
    public bool DiChuyenNgaunhien = true; // Di chuyen ngau nhien hay khong
    public Seeker seeker; // Thanh phan Seeker de tim duong
    Path path; // Duong di hien tai
    bool DichDen = false; // Co xac dinh dich den
    Coroutine Movecoroutine; // Coroutine cho viec di chuyen
    public float TocDoDiChuyen; // Toc do di chuyen cua quai vat
    public bool CapNhatDuongDi; // Cap nhat duong di lien tuc
    public float KhoangCachTiepTheo; // Khoang cach den diem tiep theo

    // Ban dan
    public GameObject Dan; // Doi tuong dan de ban
    public float TocDoDan; // Toc do di chuyen cua dan
    public float TocDoBan = 10f; // Thoi gian hoi chieu cua viec ban dan
    private float HoiChieu; // Thoi gian hoi chieu cua viec ban dan

    private void Start()
    {
        // Goi ham TinhToanDuong moi 0.5 giay de tinh toan duong di
        InvokeRepeating("TinhToanDuong", 0f, 0.5f);
        DichDen = true; // Xac dinh dich den

        // Khoi tao thoi gian hoi chieu ban dau va dat gia tri hoi chieu ban dau
        HoiChieu = Random.Range(0f, TocDoBan);
    }

    private void Update()
    {
        HoiChieu -= Time.deltaTime; // Giam thoi gian hoi chiêu theo thoi gian thuc
        if (HoiChieu <= 0)
        {
            HoiChieu = TocDoBan; // Reset thoi gian hoi chieu
            var nhanVat = FindObjectOfType<NhanVat>();
            if (nhanVat != null)
            {
                //Thuc hien hanh dong ban dan
                QuaiVatBanDan();
            }
        }
    }

    void QuaiVatBanDan()
    {
        // Tao ra mot vien dan tam thoi tai vi tri hien tai cua quai vat
        var DanTam = Instantiate(Dan, transform.position, Quaternion.identity);
        Rigidbody2D rb = DanTam.GetComponent<Rigidbody2D>();
        // Tim vi tri cua nhan vat
        Vector3 ViTriNhanVat = FindObjectOfType<NhanVat>().transform.position;
        // Tinh huong ban dan
        Vector3 PhuongHuong = ViTriNhanVat - transform.position;
        // Day dan theo huong den nhan vat
        rb.AddForce(PhuongHuong.normalized * TocDoDan, ForceMode2D.Impulse);
    }

    void TinhToanDuong()
    {
        // Tinh toan muc tieu di chuyen
        Vector2 MucTieu = TimMucTieu();
        if (seeker.IsDone() && (DichDen || CapNhatDuongDi))
        {
            // Bat dau tim duong toi muc tieu
            seeker.StartPath(transform.position, MucTieu, HoanThanhDuong);
        }
    }

    void HoanThanhDuong(Path p)
    {
        if (p.error) return; // Neu co loi thi thoat
        path = p; // Luu duong di moi
        // Bat dau di chuyen den muc tieu
        DiChuyenDenMucTieu();
    }

    void DiChuyenDenMucTieu()
    {
        if (Movecoroutine != null)
        {
            // Dung coroutine hien tai neu no dang chay
            StopCoroutine(Movecoroutine);
        }
        // Bat dau coroutine moi de di chuyen den muc tieu
        Movecoroutine = StartCoroutine(DiChuyenDenMucTieuCoroutine());
    }

    IEnumerator DiChuyenDenMucTieuCoroutine()
    {
        int Dem = 0; // Bien dem cac diem tren duong di
        DichDen = false; // Dat co DichDen la false
        while (Dem < path.vectorPath.Count)
        {
            // Tinh toan huong di chuyen
            Vector2 direction = ((Vector2)path.vectorPath[Dem] - (Vector2)transform.position).normalized;
            // Tinh toan luc di chuyen
            Vector2 force = direction * TocDoDiChuyen * Time.deltaTime;
            // Di chuyen doi tuong
            transform.position += (Vector3)force;
            // Tinh khoang cach den diem tiep theo
            float KhoangCach = Vector2.Distance(transform.position, path.vectorPath[Dem]);
            if (KhoangCach < KhoangCachTiepTheo)
            {
                Dem++; // Chuyen sang diem tiep theo
            }
            if (force.x != 0)
            {
                if (force.x > 0)
                    transform.localScale = new Vector3(0.15f, 0.15f, 0);
                else
                    transform.localScale = new Vector3(-0.15f, 0.15f, 0);


            }
            yield return null; // Doi den khung hinh tiep theo
        }
    }

    Vector2 TimMucTieu()
    {
        // Tim doi tuong nhan vat
        NhanVat nhanVat = FindObjectOfType<NhanVat>();

        if (nhanVat != null)
        {
            // Lay vi tri cua nhan vat
            Vector3 ViTriNhanVat = nhanVat.transform.position;

            if (DiChuyenNgaunhien == true)
            {
                // Neu di chuyen ngau nhien, tra ve vi tri ngau nhien xung quanh nhan vat
                return (Vector2)ViTriNhanVat + Random.Range(10f, 50f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            }
            else
            {
                // Neu khong, tra ve vi tri nhan vat
                return ViTriNhanVat;
            }
        }
        else
        {
            // Neu khong tim thay nhan vat, tra ve vi tri hien tai cua quai vat
            return transform.position;
        }
    }
}
