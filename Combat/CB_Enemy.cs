using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_Enemy : MonoBehaviour
{
    public MonsterSO monsterData;

    public int grayLevel;
    protected readonly byte[] grayValue = { 255, 204, 153, 102, 51, 0 };//从白到黑六个灰度等级


    //改变颜色
    protected virtual void IniGrayLevel()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32
            (grayValue[grayLevel], grayValue[grayLevel], grayValue[grayLevel], 255);
    }


    //返回灰度等级
    public int GetGrayLevel()
    {
        return grayLevel;
    }

    //销毁
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
