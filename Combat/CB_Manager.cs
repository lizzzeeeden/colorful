using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CB_Manager : MonoBehaviour
{
    public GameObject enemy;
    public MonsterSO mosterData;
    public float intervalTime; //生成敌方块间隔时间
    //public int enemySum;//敌方块总数

    //protected int enemyCnt;//计数


    private void Awake()
    {
        //enemyCnt = 0;
        //反复生成敌人
        InvokeRepeating("CreateEnemy", 0f, intervalTime);
    }

    //生成敌人
    protected virtual void CreateEnemy()
    {
        //达到上限退出
        //if (enemyCnt < enemySum) {
        //    Instantiate(enemy, transform);
        //    enemyCnt++;
        //} else {
        //    CancelInvoke();
        //    EndCombat();
        //}
        Instantiate(enemy, transform);
    }

    private void EndCombat()
    {
        SceneManager.LoadScene(1);
    }

}
