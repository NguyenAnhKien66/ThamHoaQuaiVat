using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLMoKhoaMap : MonoBehaviour
{
    [SerializeField] GameObject QLKhoaMap2;
    [SerializeField] GameObject QLKhoaMap3;
    [SerializeField] GameObject QLKhoaMap4;
    [SerializeField] GameObject QLKhoaMap5;
    // Start is called before the first frame update
    void Start()
    {
        MoKhoaMap();
    }
    void MoKhoaMap()
    {
        
        if(DieuKhienGame.instance!=null)
        {
            DuLieuGame gameData = DieuKhienGame.instance.duLieuGame;
            //Dieu kien mo map
            if (gameData.maps[0].DaHaDuocboss == true)
            {
                QLKhoaMap2.SetActive(false);
            }
            if (gameData.maps[1].DaHaDuocboss == true)
            {
                QLKhoaMap3.SetActive(false);
            }
            if (gameData.maps[2].DaHaDuocboss == true)
            {
                QLKhoaMap4.SetActive(false);
            }
            if (gameData.maps[3].DaHaDuocboss == true)
            {
                QLKhoaMap5.SetActive(false);
            }
        }    
            

        
       
    }    
  
}
