using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBagManager : MonoBehaviour
{
    //静态类，开单例模式，好用静态方法
    //static PlayerBagManager instance;

    [Header("UI相关")]
    public GameObject slotPrefab;
    public GameObject HPText;
    public Image HPBarFill;
    public GameObject slotModle;
    public GameObject nextButton;
    public GameObject lastButton;

    [Header("玩家数据相关")]
    public PlayerData playerData;
    public InventorySO playerInventory;
    public InventorySO playerBulletBag;

    private Transform[] itemGrid=new Transform[3];
    private Text[] bulletsText;
    private int itemGridCnt;
    public bool bFirstSelect;

    void Awake()
    {
        //索引从零开始注意，获取子弹数量text
        bulletsText = transform.Find("PlayerData").Find("Bullets").GetComponentsInChildren<Text>();
        //获取背包页
        for (int i = 0; i < 3; i++) {
            itemGrid[i] = transform.Find("ItemBack").GetChild(i);
        }

        //单例模式
        //if (!instance) {
        //    instance = this;
        //} else {
        //    Destroy(this);
        //}
    }
    void OnEnable()
    {
        itemGridCnt = 0;
        bFirstSelect = true;
        lastButton.SetActive(false);//开始没有左键

        ArrangeItem();
        FirstSelectItem();

        ShowHP();
        FillHPBar();

        ShowBulletsNum();
    }

    void Update()
    {
        FirstSelectItem();
        ChangeItemInfoText();
    }

    //把库中的东西展示在背包里，把具体物体附到slot上
    public void ArrangeItem()
    {
        //删除所有子物体
        for (int j = 0; j < 3; j++) {
            for (int i = 0; i < itemGrid[j].childCount; i++) {
                Destroy(itemGrid[j].GetChild(i).gameObject);
            }
        }

        int itemCnt=0;
        int gridCnt = 0; //不要乱改全局变量
        //将背包库里物体显示出来
        foreach (var item in playerInventory.itemList) {
            GameObject newItem = Instantiate(slotPrefab);
            newItem.transform.SetParent(itemGrid[gridCnt]);
            Slot slot = newItem.GetComponent<Slot>();
            slot.ChangeSlotImage(item.itemImage);
            slot.ChangeSlotItem(item);
            slot.transform.localScale = slotModle.transform.localScale;

            //每页12个，超出翻页，不会溢出，溢出无法将物品添加到背包中
            itemCnt++;
            if (itemCnt >= 12) {
                gridCnt++;
                itemCnt = 0;
            }
        }
    }

    private void OnDisable()
    {
        //关闭节点时清空介绍文本
        transform.Find("ItemInfoBack").GetChild(0).GetComponent<Text>().text = "";

        //背包回到第一页
        itemGrid[0].gameObject.SetActive(true);
        itemGrid[1].gameObject.SetActive(false);
        itemGrid[2].gameObject.SetActive(false);
    }

    //开始选择物品
    private void FirstSelectItem()
    {
        if (!bFirstSelect) {
            return;
        }

        //方向键或wasd键选择
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
            //没有物品则选择换页键
            if (transform.Find("ItemBack").GetChild(itemGridCnt).childCount == 0) {
                if (lastButton.activeSelf) {
                    lastButton.GetComponent<Button>().Select();
                } else {
                    nextButton.GetComponent<Button>().Select();
                }
            } else {
                transform.Find("ItemBack").GetChild(itemGridCnt).GetChild(0).GetComponent<Button>().Select();
                bFirstSelect = false;
            }
        }
    }

    //需要靠事件系统来检测被选中的物体，button里的select只是颜色过渡，就离谱
    private void ChangeItemInfoText()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
            if (!EventSystem.current.currentSelectedGameObject
                || !EventSystem.current.currentSelectedGameObject.GetComponent<Slot>()) {
                return;
            } else {
                EventSystem.current.currentSelectedGameObject.GetComponent<Slot>().OnSelect();
            }
        }
    }

    public void ShowHP()
    {
        HPText.GetComponent<Text>().text = playerData.GetHP().ToString()
            + "/"
            + playerData.GetMaxHP().ToString();
    }

    //血条
    public void FillHPBar()
    {
        HPBarFill.fillAmount = (float)playerData.GetHP() / playerData.GetMaxHP();
    }

    //显示（更新）子弹数量
    public void ShowBulletsNum()
    {
        int[] num = { 0, 0, 0, 0, 0, 0 };
        foreach (var item in playerBulletBag.itemList) {
            num[item.bBullet]++;
        }

        for (int i = 0; i < 6; i++) {
            bulletsText[i].text = num[i].ToString();
        }
    }

    public void FlipItemGrid(int n)
    {
        if (n < 0 && itemGridCnt > 0) {
            itemGridCnt--;
        } else if (n > 0 && itemGridCnt < 2) {
            itemGridCnt++;
        }

        //到顶选择键消失，代码顺序注意
        switch (itemGridCnt) {
            case 0:
                lastButton.SetActive(false);
                nextButton.SetActive(true);
                break;
            case 1:
                lastButton.SetActive(true);
                nextButton.SetActive(true);
                break;
            case 2:
                lastButton.SetActive(true);
                nextButton.SetActive(false);
                break;
        }

        for (int i = 0; i < 3; i++) {
            if (i != itemGridCnt) {
                itemGrid[i].gameObject.SetActive(false);
            } else {
                itemGrid[i].gameObject.SetActive(true);
            }
        }

        bFirstSelect = true;
        FirstSelectItem();
    }
}
