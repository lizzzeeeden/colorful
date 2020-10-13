using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripEnemy : CB_Enemy
{
    public float downSpeed;//下落速度
    
    private GameObject myCamera;

    void Awake()
    {
        //初始化颜色
        grayLevel = Random.Range(0, 6);
        IniGrayLevel();

        myCamera = GameObject.FindWithTag("MainCamera");
        SetBirthPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //设置生成位置
    private void SetBirthPosition()
    {
        Vector3 pz = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y + myCamera.GetComponent<Camera>().orthographicSize
            + gameObject.GetComponent<Collider2D>().bounds.size.y
            , 0f);
        transform.position = pz;
    }

    //长条下落
    private void Move()
    {
        Vector3 pz = gameObject.transform.position;
        pz.y -= downSpeed * Time.deltaTime;
        gameObject.transform.position = pz;
    }

   
}
