using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWorld : MonoBehaviour
{
    public ItemSO item;
    public InventorySO playerInventory;
    public PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            GetThisItem();
        }
    }

    private void GetThisItem()
    {
        if (playerData.GetBagItemCnt() >= 3 * 12) {
            return;//之后记得加提示
        }
        playerInventory.itemList.Add(item);
        Destroy(gameObject);
    }
}
