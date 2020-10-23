using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedEnemy : CB_Enemy
{
    public float speed;


    void Awake()
    {
        //初始化颜色
        grayLevel = Random.Range(0, 5);
        IniGrayLevel();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet.GetGrayLevel() > grayLevel) {
                bullet.DestroyThis();
                Destroy(gameObject);
                monsterData.ChangeHP(-1);
            } 
        }
    }

    //移动
    private void Move()
    {
        Vector3 pz = gameObject.transform.position;
        pz.x += speed * Time.deltaTime;
        gameObject.transform.position = pz;
    }

}
