using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public InventorySO playerInventory;
    public PlayerData playerData;
    public GameObject InfoText;

    private Image slotImage;
    private ItemSO item;


    private void Awake()
    {
        slotImage = gameObject.GetComponent<Image>();
    }
    public void ChangeSlotImage(Sprite i)
    {
        slotImage.sprite = i;
    }

    public void ChangeSlotItem(ItemSO i)
    {
        item = i;
    }


    //选中时更换物体的介绍
    public void OnSelect()
    {
        transform.parent.parent.parent.Find("ItemInfoBack").GetChild(0).gameObject.GetComponent<Text>().text = item.itemInfo;
    }

    //吃东西，click button
    public void EatItem()
    {
        if (!item.canUse) {
            return;
        } else if (item.bMushroom != -1) {

            int HPChange = item.HPChange;
            playerData.ChangeHP(Random.Range(-HPChange, HPChange + 1));

            Destroy(gameObject);

            //吃掉后还可以选择
            transform.parent.parent.parent.GetComponent<PlayerBagManager>().bFirstSelect = true;

            //删除背包库里的这个东西
            playerInventory.itemList.Remove(item);

            //更新血量
            PlayerBagManager playerBagManager = transform.parent.parent.parent.GetComponent<PlayerBagManager>();
            playerBagManager.ShowHP();
            playerBagManager.FillHPBar();

            //删除介绍文字
            transform.parent.parent.parent.Find("ItemInfoBack").GetChild(0).gameObject.GetComponent<Text>().text = "";
        }
    }
}
