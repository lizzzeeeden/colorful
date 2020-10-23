using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : CB_Manager
{
    public InventorySO bulletBag;
    public GameObject bulletPrefab;
    public GameObject player;

    private List<ItemSO> bullets;
    private void Awake()
    {
        bullets = bulletBag.GetList();
        //enemyCnt = 0;
        CreateBullet();
        InvokeRepeating("CreateEnemy", 0f, intervalTime);
    }


    //生成子弹
    private void CreateBullet()
    {
        foreach (var item in bullets) {
            if (item.bBullet == 0) {//吉祥物不生成
                continue;
            }
            GameObject bullet = Instantiate(bulletPrefab, player.transform);
            Bullet bulletSc = bullet.GetComponent<Bullet>();
            bulletSc.SetGrayLevel(item.bBullet);
            bulletSc.IniGrayLevel();
            bulletSc.SetItem(item);
        }
        //因为RingPlayer上有刚体，会先加载好，如果先设置active的话，那边的bullets取不到预制体实例
        //player.SetActive(true);
    }
}
