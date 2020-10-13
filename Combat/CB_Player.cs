using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_Player : MonoBehaviour
{
    protected Rigidbody2D playerRb;
    public float speed;

    public float HP;

    protected readonly byte[] grayValue = { 255, 204, 153, 102, 51, 0 };


    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("HorizontalMove"), Input.GetAxisRaw("VerticalMove"), 0.0f);
        Vector3 pz = playerRb.position;

        pz.x += movement.x * Time.deltaTime * speed;
        pz.y += movement.y * Time.deltaTime * speed;
        playerRb.MovePosition(pz);
    }

    //返回血量
    public float GetPlayerHP()
    {
        return HP;
    }

    //血量减少
    public void LoseHP(float a)
    {
        HP -= a;
    }
}
