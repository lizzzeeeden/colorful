using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedManager : CB_Manager
{
    public GameObject bullet;
    public int bulletNum;


    private void Awake()
    {
        enemyCnt = 0;
        CreateBullet();
        InvokeRepeating("CreateEnemy", 0f, intervalTime);
    }


    //生成子弹
    private void CreateBullet()
    {
        for (int i = 0; i < bulletNum; i++) {
            Instantiate(bullet, transform.parent.Find("BulletsLayout"));
        }
    }

}
