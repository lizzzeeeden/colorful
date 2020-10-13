using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughPlayer : CB_Player
{
    public int grayLevel;

    private SpriteRenderer playerSpRender;
    // Start is called before the first frame update
    void Awake()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerSpRender = gameObject.GetComponent<SpriteRenderer>();
        grayLevel = 0;
    }

    void Update()
    {
        ChangeGrayLevel();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy"
            && collision.GetComponent<CB_Enemy>().GetGrayLevel() != grayLevel) {
            LoseHP(1);
        } 
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
        playerSpRender.color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
    }

   
}
