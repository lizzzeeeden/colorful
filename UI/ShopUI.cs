using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour
{
    [Header("UI相关")]
    public Button lastButtonT;
    public Button nextButtonT;
    public Button lastButtonP;
    public Button nextButtonP;
    public Text itemIntroT;
    public Text itemIntroP;
    public Text moneyText;

    [Header("格式补齐")]
    public GameObject slotPrefab;
    public GameObject slotModle;
    public GameObject itemGridPrefab;

    [Header("数据")]
    public InventorySO traderBag;
    public InventorySO playerBag;
    public InventorySO playerBulletBag;
    public PlayerData playerData;

    public bool bFirstSelect;

    //trader
    private int itemGridCntT = 0;
    private int itemGridNumT = 0;//num计数和cnt一样，判断+1注意
    private List<Transform> itemGridT = new List<Transform>();
    private Transform itemBackT;
    //player
    private int itemGridCntP = 0;
    private int itemGridNumP = 0;
    private List<Transform> itemGridP = new List<Transform>();
    private Transform itemBackP;
    private Text[] bulletsText;


    void Awake()
    {
        itemBackT = transform.Find("Trader").Find("ItemBack");
        itemBackP = transform.Find("PlayerTrader").Find("ItemBack");
        bulletsText = transform.Find("PlayerTrader").Find("DataBack")
            .Find("BulletsData").GetComponentsInChildren<Text>();
    }
    void OnEnable()
    {
        bFirstSelect = true;
        ArrangeItem();

        ShowData();
        ShowBullets();
    }

    void Update()
    {
        FirstSelectItem();
        ChangeItemInfoText(false);
    }

    void OnDisable()
    {
        itemGridNumT = 0;
        itemGridNumP = 0;
    }


    //把库中的东西展示在界面上，把具体物体附到slot上，打开时引用一次
    public void ArrangeItem()
    {
        //界面打开时可以直接改，页面未打开时数据不同步，可以改成同步的优化，场景切换会清空注意，可以加一个判断
        //删除商店网格
        for (int i = 0; i < itemBackT.childCount; i++) {
            Destroy(itemBackT.GetChild(i).gameObject);
        }

        //删除所有玩家背包网格
        for (int i = 0; i < itemBackP.childCount; i++) {
            Destroy(itemBackP.GetChild(i).gameObject);
        }

        //将商店库里物体显示出来
        int itemCnt = 0;
        int itemGridCnt = -1; //开始创建一个

        if (traderBag.itemList.Count == 0) {//没东西也要创建一个
            itemGridNumT++;

            GameObject newItemGrid = Instantiate(itemGridPrefab);
            newItemGrid.transform.SetParent(itemBackT);
            newItemGrid.transform.localPosition = new Vector3(0, 0, 0);
            newItemGrid.transform.localScale = new Vector3(1, 1, 1);
            itemGridT.Add(newItemGrid.transform);
        } else {
            foreach (var item in traderBag.itemList) {
                //创造网格
                if (itemCnt % 8 == 0) {
                    itemGridNumT++;
                    itemGridCnt++;

                    GameObject newItemGrid = Instantiate(itemGridPrefab);
                    newItemGrid.transform.SetParent(itemBackT);
                    newItemGrid.transform.localPosition = new Vector3(0, 0, 0);
                    newItemGrid.transform.localScale = new Vector3(1, 1, 1);
                    itemGridT.Add(newItemGrid.transform);
                }

                GameObject newItem = Instantiate(slotPrefab);
                newItem.transform.SetParent(itemGridT[itemGridCnt]);

                //给slot附上物体信息
                Slot slot = newItem.GetComponent<Slot>();
                slot.ChangeSlotImage(item.itemImage);
                slot.ChangeSlotItem(item);
                slot.bTraderItem = true;
                slot.transform.localScale = slotModle.transform.localScale;

                //每页8个，超出翻页，不会溢出
                itemCnt++;
            }
        }
        //将玩家背包库里物体显示出来
        itemCnt = 0;
        itemGridCnt = -1; //开始创建一个
        if (playerBag.itemList.Count == 0) {//同样创一个
            itemGridNumP++;

            GameObject newItemGrid = Instantiate(itemGridPrefab);
            newItemGrid.transform.SetParent(itemBackP);
            newItemGrid.transform.localPosition = new Vector3(0, 0, 0);
            newItemGrid.transform.localScale = new Vector3(1, 1, 1);
            itemGridP.Add(newItemGrid.transform);
        } else {
            foreach (var item in playerBag.itemList) {
                //创造网格
                if (itemCnt % 8 == 0) {
                    itemGridNumP++;
                    itemGridCnt++;

                    GameObject newItemGrid = Instantiate(itemGridPrefab);
                    newItemGrid.transform.SetParent(itemBackP);
                    newItemGrid.transform.localPosition = new Vector3(0, 0, 0);
                    newItemGrid.transform.localScale = new Vector3(1, 1, 1);
                    itemGridP.Add(newItemGrid.transform);
                }

                GameObject newItem = Instantiate(slotPrefab);
                newItem.transform.SetParent(itemGridP[itemGridCnt]);

                //给slot附上物体信息
                Slot slot = newItem.GetComponent<Slot>();
                slot.ChangeSlotImage(item.itemImage);
                slot.ChangeSlotItem(item);
                slot.bTraderItem = false;
                slot.transform.localScale = slotModle.transform.localScale;

                //每页8个，超出翻页，不会溢出
                itemCnt++;
            }
        }

        //调整按键状态，净写些傻代码
        if (itemGridNumT > 1) {
            nextButtonT.gameObject.SetActive(true);
        } else {
            nextButtonT.gameObject.SetActive(false);
        }
        if (itemGridNumP > 1) {
            nextButtonP.gameObject.SetActive(true);
        } else {
            nextButtonP.gameObject.SetActive(false);
        }

        //调整网格状态，傻代码
        for (int i = 0; i < itemGridNumT; i++) {
            if (i == itemGridCntT) {
                itemGridT[i].gameObject.SetActive(true);
            } else {
                itemGridT[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < itemGridNumP; i++) {
            if (i == itemGridCntP) {
                itemGridP[i].gameObject.SetActive(true);
            } else {
                itemGridP[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateNewGrid(bool bTrader)//0是玩家，1是商人
    {
        if (bTrader) {//商人
            itemGridNumT++;

            GameObject newItemGrid = Instantiate(itemGridPrefab);
            newItemGrid.transform.SetParent(itemBackT);
            newItemGrid.transform.localPosition = new Vector3(0, 0, 0);
            newItemGrid.transform.localScale = new Vector3(1, 1, 1);
            itemGridT.Add(newItemGrid.transform);
            newItemGrid.SetActive(false);

            //调整按键显示状态，next键一定显示
            nextButtonT.gameObject.SetActive(true);
        } else {
            itemGridNumP++;

            GameObject newItemGrid = Instantiate(itemGridPrefab);
            newItemGrid.transform.SetParent(itemBackP);
            newItemGrid.transform.localPosition = new Vector3(0, 0, 0);
            newItemGrid.transform.localScale = new Vector3(1, 1, 1);
            itemGridP.Add(newItemGrid.transform);
            newItemGrid.SetActive(false);

            nextButtonP.gameObject.SetActive(true);
        }
    }
    public void DeleteGrid(bool bTrader)
    {
        if (bTrader) {//商人
            if (itemGridNumT == 1) {
                return;
            }

            //删除及移除当前网格
            GameObject currentGrid = itemGridT[itemGridCntT].gameObject;
            itemGridT.Remove(currentGrid.transform);
            Destroy(currentGrid);

            if (itemGridCntT >= itemGridNumT - 1) {//尾页跳转前页，非尾页跳转后页
                itemGridCntT--;
            }
            itemGridT[itemGridCntT].gameObject.SetActive(true);
            itemGridNumT--;

            //调整按键显示状态
            if (itemGridCntT == 0) {
                lastButtonT.gameObject.SetActive(false);
            }
            if (itemGridCntT == itemGridNumT - 1) {
                nextButtonT.gameObject.SetActive(false);
            }

        } else { //玩家
            if (itemGridNumP == 1) {
                return;
            }

            //删除及移除当前网格
            GameObject currentGrid = itemGridP[itemGridCntP].gameObject;
            itemGridP.Remove(currentGrid.transform);
            Destroy(currentGrid);

            if (itemGridCntP >= itemGridNumP - 1) {
                itemGridCntP--;
            }
            itemGridP[itemGridCntP].gameObject.SetActive(true);
            itemGridNumP--;

            //调整按键显示状态
            if (itemGridCntP == 0) {
                lastButtonP.gameObject.SetActive(false);
            }
            if (itemGridCntP == itemGridNumP - 1) {
                nextButtonP.gameObject.SetActive(false);
            }
        }
    }

    //需要靠事件系统来检测被选中的物体，button里的select只是颜色过渡，就离谱
    public void ChangeItemInfoText(bool bChangeNow)
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") || bChangeNow) {
            if (!EventSystem.current.currentSelectedGameObject
                || !EventSystem.current.currentSelectedGameObject.GetComponent<Slot>()) {
                return;
            } else {//这里不用的文字框直接关了
                if (EventSystem.current.currentSelectedGameObject.GetComponent<Slot>().bTraderItem) {
                    transform.Find("Trader").Find("ItemInfoBack").GetChild(0).gameObject.SetActive(true);
                    transform.Find("PlayerTrader").Find("ItemInfoBack").GetChild(0).gameObject.SetActive(false);
                } else {
                    transform.Find("Trader").Find("ItemInfoBack").GetChild(0).gameObject.SetActive(false);
                    transform.Find("PlayerTrader").Find("ItemInfoBack").GetChild(0).gameObject.SetActive(true);
                }
                EventSystem.current.currentSelectedGameObject.GetComponent<Slot>().OnSelectWithPrice();
            }
        }
    }

    //开始选择物品
    private void FirstSelectItem()
    {
        if (!bFirstSelect) {
            return;
        }

        //消除所有选择状态，在slot的TradItem里做了
        //if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null) {
        //    Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //    selectedButton.interactable = false;
        //    selectedButton.interactable = true;
        //}

        //方向键或wasd键选择
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) {
            if (itemGridT[0].childCount != 0) {
                itemGridT[itemGridCntT].GetChild(0).GetComponent<Button>().Select();
                bFirstSelect = false;
            } else if (itemGridP[0].childCount != 0) {
                itemGridP[itemGridCntP].GetChild(0).GetComponent<Button>().Select();
                bFirstSelect = false;
            }
        }
    }

    //翻页选择，两位整形，第一位代表翻页，第二位代表商人（1），玩家（0），即商人-11、11，玩家-10，10
    public void FlipItemGrid(int n)
    {
        if (n % 2 != 0) { //商人
            if (n < 0 && itemGridCntT != 0) {
                itemGridCntT--;
            } else if (n > 0 && itemGridCntT < itemGridNumT - 1) {
                itemGridCntT++;
            }

            if (itemGridCntT == 0) {
                lastButtonT.gameObject.SetActive(false);
                nextButtonT.gameObject.SetActive(true);
            } else if (itemGridCntT == itemGridNumT - 1) {
                lastButtonT.gameObject.SetActive(true);
                nextButtonT.gameObject.SetActive(false);
            } else {
                lastButtonT.gameObject.SetActive(true);
                nextButtonT.gameObject.SetActive(true);
            }
            if (itemGridNumT == 1) {
                lastButtonT.gameObject.SetActive(false);
                nextButtonT.gameObject.SetActive(false);
            }

            for (int i = 0; i < itemGridNumT; i++) {
                if (i == itemGridCntT) {
                    itemGridT[i].gameObject.SetActive(true);
                } else {
                    itemGridT[i].gameObject.SetActive(false);
                }
            }

        } else {          //玩家
            if (n < 0 && itemGridCntP != 0) {
                itemGridCntP--;
            } else if (n > 0 && itemGridCntP < itemGridNumP - 1) {
                itemGridCntP++;
            }

            if (itemGridCntP == 0) {
                lastButtonP.gameObject.SetActive(false);
                nextButtonP.gameObject.SetActive(true);
            } else if (itemGridCntP == itemGridNumP - 1) {
                lastButtonP.gameObject.SetActive(true);
                nextButtonP.gameObject.SetActive(false);
            } else {
                lastButtonP.gameObject.SetActive(true);
                nextButtonP.gameObject.SetActive(true);
            }
            if (itemGridNumP == 1) {
                lastButtonP.gameObject.SetActive(false);
                nextButtonP.gameObject.SetActive(false);
            }

            for (int i = 0; i < itemGridNumP; i++) {
                if (i == itemGridCntP) {
                    itemGridP[i].gameObject.SetActive(true);
                } else {
                    itemGridP[i].gameObject.SetActive(false);
                }
            }

        }

        bFirstSelect = true;
        FirstSelectItem();
    }

    //数据
    public void ShowData()
    {
        moneyText.text = playerData.GetMoney().ToString();
    }
    public void ShowBullets()
    {
        //显示子弹数据
        int[] num = new int[6];
        foreach (var item in playerBulletBag.itemList) {
            num[item.bBullet]++;
        }

        for (int i = 0; i < 6; i++) {
            bulletsText[i].text = num[i].ToString();
        }
    }
}
