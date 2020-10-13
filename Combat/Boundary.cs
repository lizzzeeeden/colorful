using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("feel in");
        //敌方进入销毁
        if (collision.tag == "Enemy") {
            collision.GetComponent<CB_Enemy>().DestroyThis();
        }
    }
}
