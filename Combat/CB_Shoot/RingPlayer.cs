using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPlayer : CB_Player
{
    public GameObject vacancyPrefab;//修正位置用

    private List<Transform> bullets;
    private Transform vacancyInva; //修正位置用
    private Transform vacancyVa;//修正位置用

    void Awake()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        vacancyVa = vacancyPrefab.transform;
        vacancyInva = GameObject.Find("Vacancy").transform;
        //rotateCenter = new Vector3(0, -cameraHeight / 2, 0);

        AmendChildCnt();
        ArrangeLayout();
    }

    void Update()
    {
        DialBullet();
        Shoot();
        ResetBullet();
    }

    


    //修正子弹数量
    private void AmendChildCnt()
    {
        Transform[] bulletsTmp = transform.GetComponentsInChildren<Transform>();
        bullets = new List<Transform>();
        //从1开始因为bulletsTmp[0]是父物体的组件bulletsTmp[1]是空物体
        for (int i = 2; i < bulletsTmp.Length; i++) {
            bullets.Add(bulletsTmp[i]);
        }
        if (bullets.Count % 2 == 0) {
            bullets.Add(vacancyInva);
        }
        
    }

    //整理布局，确定子弹位置
    //数学不好不要硬学编程，太快乐了
    private void ArrangeLayout()
    {
        float radius = 1f;
        float angle = 360f / bullets.Count/ 180f * Mathf.PI;//转换为弧度
        //从1开始因为以0为基准位，单数往左移，双数往右移
        for (int i = 1; i < bullets.Count; i++) {
            if (i % 2 != 0) {
                float intervalAngle = (i + 1) / 2 * angle;

                Vector3 pz = new Vector3(transform.position.x-Mathf.Sin(intervalAngle) * radius
                    , transform.position.y - (radius - Mathf.Cos(intervalAngle) * radius)
                    , 0f);
                bullets[i].position = pz;
            } else {
                float intervalAngle = i / 2 * angle;
                Vector3 pz = new Vector3(transform.position.x+Mathf.Sin(intervalAngle) * radius
                    , transform.position.y - (radius - Mathf.Cos(intervalAngle) * radius)
                    , 0f);
                bullets[i].position = pz;
            }
        }

        //0号发射位前突
        bullets[0].position = new Vector3
            (transform.position.x
            , transform.position.y + 0.5f
            , 0f);
    }

    //旋转换子弹
    //数学不好真的不要硬学编程
    private void DialBullet()
    {
        //左键
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (bullets.Count <= 1) {
                return;
            }

            int cnt = bullets.Count - 1;

            //奇数个子弹（对应最后一个序号为偶数）,偶数个子弹补充一个空位
            Transform tmp = bullets[cnt - 1];
            cnt--;

            while (cnt > 1) {
                //Vector3 tmpPz1 = bullets[cnt].position;
                //Vector3 tmpPz2 = bullets[cnt - 2].position;

                bullets[cnt] = bullets[cnt - 2];//
                //bullets[cnt - 2].position = tmpPz2;
                //if (bullets[cnt - 2].GetComponent<Bullet>()) {
                //    bullets[cnt - 2].GetComponent<Bullet>().ChangeRotateParameter
                //        (true, tmpPz1);
                //}

                cnt -= 2;//
            }
            cnt = 0;

            bullets[1] = bullets[0];
            while (cnt < bullets.Count - 1) {
                bullets[cnt] = bullets[cnt + 2];
                cnt += 2;
            }

            bullets[bullets.Count - 1] = tmp;

            ArrangeLayout();
        }

        //右键
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (bullets.Count <= 1) {
                return;
            }

            int cnt = bullets.Count - 1;

            //奇数个子弹（对应最后一个序号为偶数）,偶数个子弹补充一个空位
            Transform tmp = bullets[cnt];

            while (cnt > 0) {
                bullets[cnt] = bullets[cnt - 2];
                cnt -= 2;
            }
            cnt = 0;

            bullets[0] = bullets[1];
            cnt = 1;
            while (cnt < bullets.Count - 2) {
                bullets[cnt] = bullets[cnt + 2];
                cnt += 2;
            }

            bullets[bullets.Count - 2] = tmp;

            ArrangeLayout();
        }
    }

    //射击
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            && bullets[0].GetComponent<Bullet>()) {
            bullets[0].GetComponent<Bullet>().Shoot();

            //脱离发射子弹父子关系，空位保留成空
            bullets[0].SetParent(transform.parent);
            bullets[0] = Instantiate(vacancyVa, transform);
        }

    }

    //按E整理子弹
    private void ResetBullet()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            for (int i = 0; i < bullets.Count; i++) {
                if (bullets[i].tag == "Vacancy") {//不变的不设这个tag
                    //DestroyImmediate(bullets[i].gameObject);立刻释放内存，但读写频繁不太好
                    bullets[i].gameObject.SetActive(false);
                }
            }
            AmendChildCnt();
            ArrangeLayout();
        }
    }

  
}
