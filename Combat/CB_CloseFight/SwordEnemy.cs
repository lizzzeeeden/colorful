using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : CB_Enemy
{
    public float downSpeed;//下落速度

    private GameObject myCamera;

    void Awake()
    {
        //初始化颜色
        grayLevel = Random.Range(0, 6);
        IniGrayLevel();

        myCamera = GameObject.FindWithTag("MainCamera");
        SetBirthPosition();
        IniGrayLevel();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //设置生成位置
    private void SetBirthPosition()
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        float cameraHeight = myCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width * 1.0f / Screen.height;
        float pzX = (float)Random.Range(-10, 11) / 20;
        Vector3 pz = new Vector3(gameObject.transform.position.x+(cameraWidth- collider.bounds.size.x) *pzX,
            gameObject.transform.position.y + cameraHeight/2
            + collider.bounds.size.y
            , 0f);
        transform.position = pz;
    }

    //下落
    private void Move()
    {
        Vector3 pz = gameObject.transform.position;
        pz.y -= downSpeed * Time.deltaTime;
        gameObject.transform.position = pz;
    }

}
