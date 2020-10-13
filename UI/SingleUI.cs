using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUI : MonoBehaviour
{
    public Player player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
        player.SetPlayerMobility(true);
    }

}
