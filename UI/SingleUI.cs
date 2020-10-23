using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUI : MonoBehaviour
{
    public Player player;

    void OnEnable()
    {
        player.SetPlayerMobility(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        player.SetPlayerMobility(true);
    }
}
