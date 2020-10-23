using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int rotateSpeed;
    public int grayLevel;
    public int rotateDirection;

    private Transform player;
    private CB_Player playerSc;
    private SpriteRenderer spRender;
    private readonly byte[] grayValue = { 255, 204, 153, 102, 51, 0 };//从白到黑六个灰度等级

    void Awake()
    {
        spRender = gameObject.GetComponent<SpriteRenderer>();
        player = transform.parent;
        playerSc = player.GetComponent<CB_Player>();

        rotateDirection = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            CB_Enemy enemy = collision.GetComponent<CB_Enemy>();

            //修补子物体让父物体的trigger扩大
            //妈的到底为什么要这样设计，到底怎么分开
            playerSc.playerData.ChangeHP(1);//修复，加血抵消

            if (enemy.GetGrayLevel() == grayLevel) {
                enemy.DestroyThis();
                playerSc.monsterData.ChangeHP(-1);
            }
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        ChangeRoDirection();
        ChangeGrayLevel();
    }

    void FixedUpdate()
    {
        RotateSword();
    }

    //改变灰度函数
    private void ChangeGrayLevel()
    {
        //调整灰度级别
        if (Input.GetKeyDown(KeyCode.RightArrow) && grayLevel < 5) {
            grayLevel += 1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && grayLevel > 0) {
            grayLevel -= 1;
        }

        //改变灰度
        spRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
    }

    //剑旋转
    //神他妈加刚体之后就有问题，子物体不跟随父物体。。
    //父物体身上挂有刚体，相当于所有子物体身上都带有刚体
    //如果父物体要屏蔽子物体碰撞器/触发器，可以给子物体添加Rigidbody
    //他妈的无解了啊
    private void RotateSword()
    {
        player = transform.parent;
        transform.RotateAround(player.position,new Vector3(0,0,rotateDirection),rotateSpeed);
    }

    //改变旋转方向
    public void ChangeRoDirection()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            rotateDirection = 1;
        } else if (Input.GetKeyDown(KeyCode.E)) {
            rotateDirection = -1;
        }
    }


}
