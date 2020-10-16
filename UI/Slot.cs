using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public InventorySO playerBag;
    public InventorySO traderBag;
    public PlayerData playerData;
    public bool bTraderItem; //0为玩家物品交易，1为商人物品交易

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
        transform.parent.parent.parent.Find("ItemInfoBack").GetChild(0).GetComponent<Text>().text
            = item.itemInfo;
    }
    //选中时更换物体的介绍（带价格）
    public void OnSelectWithPrice()
    {
        transform.parent.parent.Find("ItemInfoBack").GetChild(0).GetComponent<Text>().text
            = item.itemInfo + "\n" + item.price.ToString();
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
            playerBag.itemList.Remove(item);

            //更新血量
            PlayerBagManager playerBagManager = transform.parent.parent.parent.GetComponent<PlayerBagManager>();
            playerBagManager.ShowData();

            //删除介绍文字
            transform.parent.parent.parent.Find("ItemInfoBack").GetChild(0).gameObject.GetComponent<Text>().text = "";
        }
    }

    //交易物品，click button
    public void TradeItem()
    {
        if (bTraderItem) {//商人
            if (playerData.GetMoney() < item.price) {
                return;//钱不够，后加显示
            }
            if (playerData.GetBagItemCnt() >= 3 * 12) {
                return;//背包满，后加显示
            }

            //数据更新
            playerData.ChangeMoney(-item.price);
            traderBag.itemList.Remove(item);
            playerBag.itemList.Add(item);
            bTraderItem = false;//交换后修改数据注意

            //UI显示
            Transform itemBack = transform.parent.parent.parent.parent.Find("PlayerTrader").Find("ItemBack");
            if (itemBack.GetChild(itemBack.childCount - 1).childCount >= 8) {//尾页是否满
                transform.parent.parent.parent.parent.GetComponent<ShopUI>().CreateNewGrid(bTraderItem);
                transform.SetParent(itemBack.GetChild(itemBack.childCount - 1));
            } else if (transform.parent.childCount == 1) {//只剩这一个，交易后删除页
                transform.SetParent(itemBack.GetChild(itemBack.childCount - 1));
                transform.parent.parent.parent.parent.GetComponent<ShopUI>().DeleteGrid(!bTraderItem);//这里判断是之前的注意，因此相反
            } else {
                transform.SetParent(itemBack.GetChild(itemBack.childCount - 1));
            }

        } else {

            //数据更新
            playerData.ChangeMoney(item.price);
            playerBag.itemList.Remove(item);
            traderBag.itemList.Add(item);
            bTraderItem = true;

            //UI显示
            Transform itemBack = transform.parent.parent.parent.parent.Find("Trader").Find("ItemBack");
            if (itemBack.GetChild(itemBack.childCount - 1).childCount >= 8) {//尾页是否满
                transform.parent.parent.parent.parent.GetComponent<ShopUI>().CreateNewGrid(bTraderItem);
                transform.SetParent(itemBack.GetChild(itemBack.childCount - 1));//顺序不一样，很重要
            } else if (transform.parent.childCount == 1) {//只剩这一个
                transform.SetParent(itemBack.GetChild(itemBack.childCount - 1));
                transform.parent.parent.parent.parent.GetComponent<ShopUI>().DeleteGrid(!bTraderItem);
            } else {
                transform.SetParent(itemBack.GetChild(itemBack.childCount - 1));
            }

        }
    }
}
