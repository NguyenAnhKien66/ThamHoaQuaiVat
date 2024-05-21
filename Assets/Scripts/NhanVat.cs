using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhanVat : MonoBehaviour
{
    //public SpriteRenderer characterSR;// khai bao tham chieu den mot thanh phan SpriteRenderer
    public float TocDoDiChuyen = 5f; // Khai bao toc do di chuyen mac dinh
    public float TocDoCuon = 2f; // khai bao toc do cuon
    public float ThoiGianCuon; // khai bao thoi gian cuon
    private Rigidbody2D rb; 
    public Vector3 HuongDiChuyen;// khai bao huong di chuyen nhan vat
    public float HoiChieuCuon = 10f; // Thoi gian cho giua cac lan cuon
    private float ThoiGianHoiChieu = 0f; // dem thoi gian con lai cho cooldown
    private bool DangCuon = false; // Bien de kiem tra xem dang cuon tron hay khong
    public Animator animator;
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

        HuongDiChuyen.x = Input.GetAxis("Horizontal");
        HuongDiChuyen.y = Input.GetAxis("Vertical");
        //vi tri nhan vat
        transform.position += HuongDiChuyen * TocDoDiChuyen * Time.deltaTime;
        //characterSR.GetComponent<Animator>().SetFloat("Speed", HuongDiChuyen.sqrMagnitude);
        animator.SetFloat("TocDo", HuongDiChuyen.sqrMagnitude);
        // Kiem tra neu khong dang trong qua trinh cuon va ThoiGianHoiChieu da het
        if (!DangCuon && ThoiGianHoiChieu <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //characterSR.GetComponent<Animator>().SetBool("Roll", true);
                TocDoDiChuyen += TocDoCuon;
                ThoiGianHoiChieu = HoiChieuCuon; // Reset ThoiGianHoiChieu
                DangCuon = true; // Bat dau thuc hien cuon
                StartCoroutine(StopRollingAfterTime(ThoiGianCuon)); // Goi ham dung cuon sau mot khoang thoi gian nhat dinh
            }
        }

        // Giam ThoiGianHoiChieu neu no khong bang 0
        if (ThoiGianHoiChieu > 0)
        {
            ThoiGianHoiChieu -= Time.deltaTime;
        }

        // Xu ly huong nhan vat
        if (HuongDiChuyen.x != 0)
        {
            if (HuongDiChuyen.x > 0)
            {
                //characterSR.transform.localScale = new Vector3(1f, 1f, 0);
                transform.localScale = new Vector3(0.15f, 0.15f, 0);
            }
            else
            {
                //characterSR.transform.localScale = new Vector3(-1f, 1f, 0);
                transform.localScale = new Vector3(-0.15f, 0.15f, 0);
            }
        }
    }

    // Khai bao mot IEnumerator co ten la StopRollingAfterTime, nhan vao mot tham so float time
    IEnumerator StopRollingAfterTime(float time)
    {
        // Tam dung coroutine trong khoang thoi gian duoc chi dinh (time) tinh bang giay
        yield return new WaitForSeconds(time);

        // Lay thanh phan Animator cua characterSR va dat gia tri bien bool "Roll" thanh false
        //characterSR.GetComponent<Animator>().SetBool("Roll", false);

        // Giam TocDoDiChuyen bang gia tri ThoiGianCuon
        TocDoDiChuyen -= ThoiGianCuon;

        // Dat gia tri DangCuon thanh false de cho biet nhan vat khong con dang cuon nua
        DangCuon = false;
    }

}
