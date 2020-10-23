using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shootSpeed;
    public InventorySO bulletBag;
    private ItemSO item;
    private int grayLevel;

    //public float rotateSpeed;//转位置用
    //private Vector3 endPosition;
    //private bool bRotateBegin;

    private SpriteRenderer bulletSpRender;
    private bool bShoot;
    private readonly byte[] grayValue = { 255, 204, 153, 102, 51, 0 };

    void Awake()
    {
        GetComponent<Collider2D>().enabled=false;//主要使ring模式直接碰到怪不会消失
        //grayLevel = Random.Range(1, 6);
        bulletSpRender = GetComponent<SpriteRenderer>();
        bShoot = false;
        //bRotateBegin = false;
    }

    void Update()
    {
        Move();
    }

    //private void FixedUpdate()
    //{
    //    ChangeSeat();
    //}

    public void IniGrayLevel()
    {
        bulletSpRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
    }

    public int GetGrayLevel()
    {
        return grayLevel;
    }
    public void SetGrayLevel(int i)
    {
        grayLevel = i;
    }

    public void SetItem(ItemSO i)
    {
        item = i;
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    //发射子弹的前设
    public void Shoot()
    {
        bShoot = true;
        GetComponent<Collider2D>().enabled=true;
        bulletBag.GetList().Remove(item);
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
