using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public PlayerData playerData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("feel in");
        //敌方进入销毁
        if (collision.tag == "Enemy") {
            collision.GetComponent<CB_Enemy>().DestroyThis();
        }else if (collision.CompareTag("Bullet")) {
            collision.GetComponent<Bullet>().DestroyThis();
        }else if (collision.CompareTag("LimitedEnemy")) {
            playerData.ChangeHP(-1);
            collision.GetComponent<CB_Enemy>().DestroyThis();
        }
    }
}
