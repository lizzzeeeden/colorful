using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedManager : CB_Manager
{
    public InventorySO bulletBag;
    public GameObject bulletPrefab;
    public LimitedLayout layoutSC;

    private List<ItemSO> bullets;

    private void Awake()
    {
        bullets = bulletBag.GetList();
        //enemyCnt = 0;
        CreateBullet();
        InvokeRepeating("CreateEnemy", 0f, intervalTime);
        //调用的顺序逻辑，所以放在这里调用
        layoutSC.AmendChildCnt();
        layoutSC.ArrangeLayout();
    }

    //生成子弹
    private void CreateBullet()
    {
        foreach (var item in bullets) {
            if (item.bBullet == 0) {//吉祥物不生成
                continue;
            }
            GameObject bullet=Instantiate(bulletPrefab, transform.parent.Find("BulletsLayout"));
            Bullet bulletSc = bullet.GetComponent<Bullet>();
            bulletSc.SetGrayLevel(item.bBullet);
            bulletSc.IniGrayLevel();
            bulletSc.SetItem(item);
        }
    }

}
