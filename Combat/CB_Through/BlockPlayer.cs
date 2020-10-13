using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer : ThroughPlayer
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            CB_Enemy enemy = collision.GetComponent<CB_Enemy>();
            if (enemy.GetGrayLevel()!= grayLevel) {
                LoseHP(1);
            } else {
                enemy.DestroyThis();
            }
        }
    }
}
