using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPlayer : CB_Player
{
    public int levelUpInterval;

    private int fishCatchCnt;
    private SpriteRenderer playerSpRender;
    private CB_Player playerSc;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSpRender = GetComponent<SpriteRenderer>();
        playerSc = GetComponent<CB_Player>();

        grayLevel = 1;
        ChangeGrayLevel();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
        CB_Enemy enemy = collision.GetComponent<CB_Enemy>();

            //颜色比它更深可以吃
            if (enemy.GetGrayLevel() > grayLevel) {
                playerData.ChangeHP(-1);
                enemy.DestroyThis();
            } else {
                fishCatchCnt++;
                GrayLevelUP();
                enemy.DestroyThis();
                playerSc.monsterData.ChangeHP(-1);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    //颜色改变
    private void ChangeGrayLevel()
    {
        playerSpRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
    }

    //移动函数
    protected override void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);

        //转向
        if (movement.x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (movement.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 pz = playerRb.position;


        pz.x += movement.x * Time.deltaTime * speed;
        pz.y += movement.y * Time.deltaTime * speed;
        playerRb.MovePosition(pz);
    }

    //增加等级
    private void GrayLevelUP()
    {
        if (fishCatchCnt % levelUpInterval == 0 && grayLevel < 5) {
            grayLevel++;
            ChangeGrayLevel();
        }
    }

}
