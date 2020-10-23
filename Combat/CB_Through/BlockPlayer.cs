using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer : ThroughPlayer//继承着，别乱复写
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            CB_Enemy enemy = collision.GetComponent<CB_Enemy>();
            if (enemy.GetGrayLevel()!= grayLevel) {
                playerData.ChangeHP(-1);
            } else {
                enemy.DestroyThis();
                playerSc.monsterData.ChangeHP(-1);
            }
        }
    }
}
