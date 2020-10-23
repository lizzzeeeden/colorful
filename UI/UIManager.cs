using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject dialogUI;

    public SingleUI bagUI;
    public SingleUI pauseUI;
    public Player player;
    //开关ui界面相关

    void Update()
    {
        OpenBag();
        OpenPauseUI();
    }

    //打开背包
    private void OpenBag()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!bagUI.isActiveAndEnabled && !dialogUI.activeSelf) {
                bagUI.gameObject.SetActive(true);
            } else {
                bagUI.gameObject.SetActive(false);
            }
        }
    }

    //暂停界面
    private void OpenPauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && !bagUI.isActiveAndEnabled
            && !pauseUI.isActiveAndEnabled) {
            pauseUI.gameObject.SetActive(true);
        }
    }
}
