using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : CB_Manager
{
    public GameObject bullet;
    public GameObject player;
    public int bulletNum;

    private void Awake()
    {
        enemyCnt = 0;
        CreateBullet();
        InvokeRepeating("CreateEnemy", 0f, intervalTime);
    }


    //生成子弹，因为RingPlayer上有刚体，会先加载好，如果先设置active的话，那边的bullets取不到预制体实例
    private void CreateBullet()
    {
        for (int i = 0; i < bulletNum; i++) {
            Instantiate(bullet, player.transform);
        }
        player.SetActive(true);
    }
}
