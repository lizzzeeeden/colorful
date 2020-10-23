using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public CharacterSO character;
    //UI相关
    private GameObject canvas;
    private GameObject tipUI;
    private GameObject shopUI;
    private GameObject dialogUI;



    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        tipUI = canvas.transform.Find("Tips").Find("Talk_F").gameObject;
        shopUI = canvas.transform.Find("ShopUI").gameObject;
        dialogUI = canvas.transform.Find("DialogUI").gameObject;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            tipUI.SetActive(true);
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            tipUI.SetActive(false);
        }
    }

    void Update()
    {
        if (tipUI.activeSelf && Input.GetKeyDown(KeyCode.F)) {
            if (character.GetBTrader() != -1) {//商店
                Trade();
            } else {//路人
                Talk();
            }
        }
    }

    //开启对话
    private void Talk()
    {
        dialogUI.SetActive(true);
        dialogUI.GetComponent<DialogUI>().SetText(character.GetText());
    }

    //开启商店
    private void Trade()
    {
        shopUI.SetActive(true);
    }
}
