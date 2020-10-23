using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemy : CB_Enemy
{
    public float moveSpeed;//移动速度
    public float darkenTime;

    private SpriteRenderer playerSpRender;
    private GameObject myCamera;
    private float speed;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        myCamera = GameObject.FindWithTag("MainCamera");
        playerSpRender = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        moveSpeed = Random.Range(1.0f, 7.0f);
        speed = 0;

        //初始化颜色
        IniGrayLevel();

        //初始化方向
        if (Random.Range(0, 2) == 1) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        IniPosition();
    }

    void Update()
    {
        FishMove();
    }

    //颜色初始化
    protected override void IniGrayLevel()
    {
        //设定出生颜色
        playerSpRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
        InvokeRepeating("AddGrayLevel", darkenTime, darkenTime);
        grayLevel = 0;
    }

    //变深
    private void AddGrayLevel()
    {
        if (speed > 0) {//有速度了不用继续
            return;
        }
        if (grayLevel < 5) {
            grayLevel++;
            
        } else {
            //到达最黑开始追逐主角，设定速度
            CancelInvoke();
            speed = moveSpeed;
        }
        playerSpRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
        InvokeRepeating("AddGrayLevel", darkenTime, darkenTime);
    }

    //位置初始化
    private void IniPosition()
    {
        transform.position = new Vector3(0, 0, 0);

        float cameraHeight = myCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width * 1.0f / Screen.height;
        float blockSizeX = GetComponent<Collider2D>().bounds.size.x;
        float blockSizeY = GetComponent<Collider2D>().bounds.size.y;

        float pzX = (float)Random.Range(-10, 11) / 20;
        float pzY = (float)Random.Range(-5, 6) / 10;

        Vector3 pz = new Vector3
            (transform.position.x + pzX * (cameraWidth - blockSizeX),
            transform.position.y + pzY * (cameraHeight - blockSizeY),
            0f);

        transform.position = pz;
    }

    //移动
    private void FishMove()
    {
        //追逐主角方块
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPz = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPz, speed * Time.deltaTime);

        //转向
        if (speed > 0.1) {
            if (playerPz.x > transform.position.x) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
