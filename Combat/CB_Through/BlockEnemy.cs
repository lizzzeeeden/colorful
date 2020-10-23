using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : CB_Enemy
{
    public float speed;//移动速度

    private GameObject myCamera;
    private enum direction { UP, DOWN, LEFT, RIGHT };//四个方位
    private direction birthDirection;//生成的方位
    void Awake()
    {
        //初始化颜色
        grayLevel = Random.Range(0, 6);
        IniGrayLevel();

        myCamera = GameObject.FindWithTag("MainCamera");
        birthDirection = (direction)Random.Range(0, 4);
        SetBirthPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //生成位置（相机的位置比较麻烦，阿狗累了，真的累了）
    private void SetBirthPosition()
    {
        //先初始化一下，不然会有谜之偏移
        Vector3 iniPz = new Vector3(0, 0, 0);
        transform.position = iniPz;

        float cameraHeight = myCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraWidth = cameraHeight * Screen.width * 1.0f / Screen.height;
        float blockSizeX= gameObject.GetComponent<Collider2D>().bounds.size.x;
        float blockSizeY= gameObject.GetComponent<Collider2D>().bounds.size.y;

        //根据不同生成方位调整出现位置
        switch (birthDirection) {
            case direction.UP:
                float pzX1 = (float)Random.Range(-10, 11) / 20;
                Vector3 pz1 = new Vector3
                    (transform.position.x +pzX1 * (cameraWidth- blockSizeX),
                    transform.position.y + cameraHeight/2 + blockSizeY, 
                    0f);
                transform.position = pz1;
                break;
            case direction.DOWN:
                float pzX2 = (float)Random.Range(-10, 11) / 20;
                Vector3 pz2 = new Vector3
                    (transform.position.x +pzX2 * (cameraWidth - blockSizeX),
                    transform.position.y - cameraHeight/2- blockSizeY,
                    0f);
                transform.position = pz2;
                break;
            case direction.LEFT:
                float pzY1 = (float)Random.Range(-10, 11) / 20;
                Vector3 pz3 = new Vector3
                    (transform.position.x - cameraWidth/2- blockSizeX,
                   transform.position.y + pzY1 * (cameraHeight- blockSizeY)
                   , 0f);
                transform.position = pz3;
                break;
            case direction.RIGHT:
                float pzY2 = (float)Random.Range(-10, 11) / 20;
                Vector3 pz4 = new Vector3
                    (transform.position.x + cameraWidth / 2+ blockSizeX,
                   transform.position.y + pzY2 * (cameraHeight- blockSizeY)
                   , 0f);
                transform.position = pz4;
                break;
        }
    }

    //移动
    private void Move()
    {
        Vector3 pz = gameObject.transform.position;
        switch (birthDirection) {
            case direction.UP:
                pz.y -= speed * Time.deltaTime;
                break;
            case direction.DOWN:
                pz.y += speed * Time.deltaTime;
                break;
            case direction.LEFT:
                pz.x += speed * Time.deltaTime;
                break;
            case direction.RIGHT:
                pz.x -= speed * Time.deltaTime;
                break;
        }

        gameObject.transform.position = pz;
    }

}
