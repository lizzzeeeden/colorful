using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SingleUI bagUI;
    public SingleUI pauseUI;
    public Player player;
    //开关ui界面相关

    void Update()
    {
        OpenBag();
        OpenPauseUI();
    }
    private void OpenBag()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!bagUI.isActiveAndEnabled) {
                bagUI.gameObject.SetActive(true);
                player.SetPlayerMobility(false);
            } else {
                bagUI.gameObject.SetActive(false);
                player.SetPlayerMobility(true);
            }
        }
    }
    private void OpenPauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            &&!bagUI.isActiveAndEnabled
            && !pauseUI.isActiveAndEnabled) {         
                pauseUI.gameObject.SetActive(true);
                player.SetPlayerMobility(false);
        }
    }
}
