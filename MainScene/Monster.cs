using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public PlayerData playerData;
    public MonsterSO monsterData;
    public MonsterSO thisMonster;

    private Vector3 position;

    void Awake()
    {
        if (monsterData.bKilled) {
            gameObject.SetActive(false);
        }
        position = transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            playerData.SetPosition(position);
            Combat();
        }
    }

    private void Combat()
    {
        //传递参数，跨场景调用
        monsterData.SetCombatType(thisMonster.GetCombatType());
        monsterData.SetHP(thisMonster.GetHP());
        SceneManager.LoadScene("Combat");
    }

    public Vector3 GetPosition()
    {
        return position;
    }
}
