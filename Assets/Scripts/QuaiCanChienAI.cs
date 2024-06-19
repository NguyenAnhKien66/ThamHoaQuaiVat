using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaiCanChienAI : MonoBehaviour
{
    public Seeker seeker;
    Path path;

    Coroutine DiChuyenCoroutine;
    public float TocDoDiChuyen;
    public float KhoangCachDiemKeTiep;
    

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("TinhToanDuongDi", 0f, 0.5f);
    }
    //Tinh toan duong di
    void TinhToanDuongDi()
    {
        Vector2 MucTieu = TimMucTieu();
        if(seeker.IsDone())
        {
            seeker.StartPath(transform.position,MucTieu,HoanThanhDuong);
        }    
    }
    Vector2 TimMucTieu()
    {
        Vector3 ViTriNhanVat = FindObjectOfType<NhanVat>().transform.position;
        return ViTriNhanVat;
    }
    void HoanThanhDuong(Path p)
    {
        if (p.error) return;
        path = p;
        Debug.Log("path = " + path);
        //Di chuyen den muc tieu
        DiChuyenDenMucTieu();
    }
    void DiChuyenDenMucTieu()
    {
        if (DiChuyenCoroutine != null) StopCoroutine(DiChuyenCoroutine);
        DiChuyenCoroutine = StartCoroutine(DiChuyenDenMucTieuCoroutine());
    }
    IEnumerator DiChuyenDenMucTieuCoroutine()
    {
        int Diem = 0;
        
        while(Diem<path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[Diem]-(Vector2)transform.position).normalized;
            Vector3 force=direction*TocDoDiChuyen*Time.deltaTime;
            transform.position += force;

            //Animation khi quai di chuyen
            GetComponent<Animator>().SetFloat("TocDo", direction.sqrMagnitude);

            float KhoangCach = Vector2.Distance(transform.position, path.vectorPath[Diem]);
            if(KhoangCach < KhoangCachDiemKeTiep)
            {
                Diem++;
            }
            if(force.x!=0)
            {
                if (force.x > 0)
                    transform.localScale = new Vector3(0.15f, 0.15f, 0);
                else
                    transform.localScale = new Vector3(-0.15f, 0.15f, 0);


            }
            yield return null;
        }
    }   
}
