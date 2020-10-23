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
    private Button button;


    private void Awake()
    {
        slotImage = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
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
        transform.parent.parent.parent.Find("ItemInfoBack").GetChild(0).GetComponent<Text>().text
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


            BagUI bag = transform.parent.parent.parent.GetComponent<BagUI>();
            //吃掉后还可以选择
            bag.bFirstSelect = true;
            bag.ShowData(); //更新血量

            //删除背包库里的这个东西
            playerBag.itemList.Remove(item);

            //删除介绍文字
            transform.parent.parent.parent.Find("ItemInfoBack").GetChild(0).gameObject.GetComponent<Text>().text = "";

            Destroy(gameObject);//内存还没释放
        }
    }

    //交易物品，click button
    public void TradeItem()
    {
        ShopUI shop = transform.parent.parent.parent.parent.GetComponent<ShopUI>();

        if (bTraderItem) {//商人物品
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
            button.interactable = false;//更新选择状态
            button.interactable = true;

            

            //UI显示
            Transform itemBackP = transform.parent.parent.parent.parent.Find("PlayerTrader").Find("ItemBack");
            if (itemBackP.GetChild(itemBackP.childCount - 1).childCount >= 8) {//尾页是否满
                shop.CreateNewGrid(bTraderItem);
                transform.SetParent(itemBackP.GetChild(itemBackP.childCount - 1));

                //更新选择的物品
                if (transform.GetComponent<Button>().FindSelectableOnRight()) {
                    transform.GetComponent<Button>().FindSelectableOnRight().Select();
                } else {
                    transform.GetComponent<Button>().FindSelectableOnLeft().Select();
                }
            } else if (transform.parent.childCount == 1) {//只剩这一个，交易后删除页
                transform.SetParent(itemBackP.GetChild(itemBackP.childCount - 1));
                shop.DeleteGrid(!bTraderItem);//这里判断是之前的注意，因此相反

                //需要重新选择
                shop.bFirstSelect = true;
            } else {
                transform.SetParent(itemBackP.GetChild(itemBackP.childCount - 1));

                //更新选择的物品
                if (transform.GetComponent<Button>().FindSelectableOnRight()) {
                    transform.GetComponent<Button>().FindSelectableOnRight().Select();
                } else {
                    transform.GetComponent<Button>().FindSelectableOnLeft().Select();
                }
            }

        } else {//玩家物品

            //数据更新
            playerData.ChangeMoney(item.price);
            playerBag.itemList.Remove(item);
            traderBag.itemList.Add(item);
            bTraderItem = true;
            button.interactable = false;//更新选择状态
            button.interactable = true;



            //UI显示
            Transform itemBackT = transform.parent.parent.parent.parent.Find("Trader").Find("ItemBack");
            if (itemBackT.GetChild(itemBackT.childCount - 1).childCount >= 8) {//尾页是否满
                shop.CreateNewGrid(bTraderItem);
                transform.SetParent(itemBackT.GetChild(itemBackT.childCount - 1));//顺序不一样，很重要

                //更新选择的物品
                if (button.FindSelectableOnRight()) {
                    button.FindSelectableOnRight().Select();
                } else {
                    button.FindSelectableOnLeft().Select();
                }
            } else if (transform.parent.childCount == 1) {//只剩这一个
                transform.SetParent(itemBackT.GetChild(itemBackT.childCount - 1));
                shop.DeleteGrid(!bTraderItem);

                //需要重新选择
                shop.bFirstSelect = true;
            } else {
                transform.SetParent(itemBackT.GetChild(itemBackT.childCount - 1));

                //更新选择的物品
                if (button.FindSelectableOnRight()) {
                    button.FindSelectableOnRight().Select();
                } else {
                    button.FindSelectableOnLeft().Select();
                }
            }

        }

        //更新钱
        shop.ShowData();
        shop.ChangeItemInfoText(true);
    }
}
