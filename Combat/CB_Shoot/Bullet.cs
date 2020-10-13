using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shootSpeed;

    //public float rotateSpeed;//转位置用
    //private Vector3 endPosition;
    //private bool bRotateBegin;

    private SpriteRenderer bulletSpRender;
    private readonly byte[] grayValue = { 255, 204, 153, 102, 51, 0 };//从白到黑六个灰度等级
    private int grayLevel;
    private bool bShoot;


    void Awake()
    {
        grayLevel = Random.Range(1, 6);
        bulletSpRender = gameObject.GetComponent<SpriteRenderer>();
        bShoot = false;
        //bRotateBegin = false;

        ChangeGrayLevel(grayLevel);

    }

    void Update()
    {
        Move();
    }

    //private void FixedUpdate()
    //{
    //    ChangeSeat();
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            CB_Enemy enemy = collision.GetComponent<CB_Enemy>();
                if (enemy.GetGrayLevel() < grayLevel) {
                    enemy.DestroyThis();
                    Destroy(gameObject);
                }
        }
    }


    public void ChangeGrayLevel(int gl)
    {
        grayLevel = gl;
        bulletSpRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
    }

    public int GetGrayLevel()
    {
        return grayLevel;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //发射子弹
    public void Shoot()
    {
        bShoot = true;
        gameObject.GetComponent<Collider2D>().enabled=true;
    }

    private void Move()
    {
        if (bShoot) {
            Vector3 pz = transform.position;
            pz.y += shootSpeed * Time.deltaTime;
            transform.position = pz;
        }
    }

    //private void ChangeSeat()
    //{
    //    if (!bRotateBegin) {
    //        return;
    //    } else {
    //        Debug.Log(transform.position);
    //    }
    //    //if (transform.position == endPosition) {
    //    //    bRotateBegin = false;
    //    //    return;
    //    //}
    //    transform.position = Vector2.MoveTowards
    //        (transform.position, endPosition, rotateSpeed * Time.deltaTime);
    //}

    //public void ChangeRotateParameter(bool bRotateBeginTmp, Vector3 endPositionTmp)
    //{
    //    endPosition = endPositionTmp;
    //    bRotateBegin = bRotateBeginTmp;
    //    Debug.Log(transform.position);
    //}
}
