using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_Player : MonoBehaviour
{
    public float speed;
    public int grayLevel;

    public PlayerData playerData;
    public MonsterSO monsterData;

    protected Rigidbody2D playerRb;
    protected readonly byte[] grayValue = { 255, 204, 153, 102, 51, 0 };

    private void FixedUpdate()
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

}
