using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingEnemy : CB_Enemy
{
    public float speed;

    private GameObject myCamera;
    void Awake()
    {
        myCamera = GameObject.FindWithTag("MainCamera");

        //初始化颜色
        grayLevel = Random.Range(0, 5);
        IniGrayLevel();

        SetBirthPosition();
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
        }else if (collision.CompareTag("Player")) {

        }
    }

    //初始位置
    private void SetBirthPosition()
    {
        //先初始化一下，不然会有谜之偏移
        Vector3 iniPz = new Vector3(0, 0, 0);
        transform.position = iniPz;

        float cameraHeight = myCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width * 1.0f / Screen.height;
        float blockSizeX = gameObject.GetComponent<Collider2D>().bounds.size.x;
        float blockSizeY = gameObject.GetComponent<Collider2D>().bounds.size.y;


        float pzX1 = (float)Random.Range(-10, 11) / 20;
        Vector3 pz1 = new Vector3
            (transform.position.x + pzX1 * (cameraWidth - blockSizeX),
            transform.position.y + cameraHeight / 2 + blockSizeY,
            0f);
        transform.position = pz1;

    }



//移动
private void Move()
{
    Vector3 pz = gameObject.transform.position;
    pz.y -= speed * Time.deltaTime;
    gameObject.transform.position = pz;
}

 
}
