using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPlayer : CB_Player
{

    Collider2D coll;


    void Awake()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject. GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coll.tag=="Player"&&collision.tag == "Enemy") {
            LoseHP(1);
        }
    }

}
