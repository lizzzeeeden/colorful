using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPlayer : CB_Player
{

    Collider2D coll;


    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coll.tag=="Player"&&collision.tag == "Enemy") {
            playerData.ChangeHP(-1);
        }
    }

}
