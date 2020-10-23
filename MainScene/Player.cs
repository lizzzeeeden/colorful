using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家相关")] //血量等数据在PlayerData里
    public float speed;
    public  PlayerData playerData;
    public InventorySO objectBag;
    public InventorySO bulletBag;

    private Rigidbody2D player;
    private Animator anim;
    private bool canPlayerMove;


    // Start is called before the first frame update
    void Awake()
    {
        canPlayerMove = true;

        player = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (playerData.GetPosition() != Vector3.zero) {
            transform.position = Vector3.zero;
        }
        playerData.SetPosition(Vector3.zero);
    }

    void FixedUpdate()
    {
        Move();
    }

    //移动函数
    private void Move()
    {
        if (!canPlayerMove) {
            return;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        Vector3 pz = player.position;

        pz.x += movement.x * Time.deltaTime*speed;
        pz.y += movement.y * Time.deltaTime*speed;
        player.MovePosition(pz);

        //动画相关
        if (movement.magnitude != 0) {
            anim.SetFloat("dirX", movement.x);
            anim.SetFloat("dirY", movement.y);
        }
        anim.SetFloat("move", movement.magnitude);
    }

    public void SetPlayerMobility(bool b)
    {
        canPlayerMove = b;
    }

}
