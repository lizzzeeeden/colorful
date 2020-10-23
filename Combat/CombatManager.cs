using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public PlayerData playerData;
    public MonsterSO monsterData;
    public GameObject[] combatNode = new GameObject[6];

    void OnEnable()
    {
        combatNode[monsterData.GetCombatType()].SetActive(true);
    }
    void Update()
    {
        if (playerData.GetHP() <= 0) {
            EndCombat();
        }else if (monsterData.GetHP() <= 0) {
            monsterData.bKilled = true;
            EndCombat();
        }
    }
    public static void EndCombat()
    {
        SceneManager.LoadScene(1);
    }
}
