using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviourPun
{
    public InventoryUI UI;

	
    public int spaceLimit;
    public Player currentPlayer;
    Item containerForRpc;


    public List<ItemData> items = new List<ItemData>();

    public void InitInventory(int level)
    {
        spaceLimit *= (level+1);
        UI.InitUI();
    }

    public void AddItem(Item item)
    {
        if (items.Count >= spaceLimit)
        {
            Debug.Log("Not enough room.");
            return;
        }

        foreach(ItemData i in items)
        {
            Item tmp = i.prefab.GetComponent<Item>();
            if(tmp.GetType().Name == item.GetType().Name && item.itemName == tmp.itemName)
            {
                int remainingslot = tmp.maxStack - tmp.amount;
                int getIn = Mathf.Min(remainingslot, item.amount);
                item.amount -= getIn;
                i.amount += getIn;
            }
        }
        if(item.amount > 0)
        {
            if(item is IUsable)
            {
                UsableItemData newitem = new UsableItemData();
                newitem.prefab = item.prefab;
                newitem.amount = item.amount;
                newitem.Name = item.itemName;
                items.Add(newitem);
            }
            else
            {
                ItemData newitem = item.to_data();
                items.Add(newitem);
            }            
        }

        UI.UpdateUI();
    }

    public void Remove(ItemData item)
    {
        // //item.itemObject = Instantiate(item.prefab, transform.position + (transform.forward * 2), transform.rotation);
        // item.itemObject = PhotonNetwork.Instantiate("Prefabs/" + item.prefab.name, transform.position + (transform.forward * 2), transform.rotation);
        // item.itemObject.GetPhotonView().TransferOwnership(PhotonNetwork.MasterClient);
        // containerForRpc = item;
        // photonView.RPC("rpc_configure_item", RpcTarget.All);
    }

    public ItemData[] getAllItem()
    {
        ItemData[] allitems = new ItemData[items.Count];
        //convert item to gameobject
        for(int i = 0; i < items.Count; i++)
        {
            allitems[i] = items[i];
        }
        return allitems;
    }

    [PunRPC]
    public void rpc_configure_item()
    {
        Item item = containerForRpc;
        item.itemObject.GetComponent<Item>().amount = item.amount;
        item.itemObject.GetComponent<Item>().prefab = item.prefab;
        // items.Remove(item);
        UI.UpdateUI();
    }

    public bool isFull()
    {
        return (items.Count >= spaceLimit);
    }

    public void removeAllZeroItem()
    {
        items.RemoveAll(t => t.amount <= 0);
        UI.UpdateUI();
    }

    public void UseItemInInventory(UsableItemData item)
    {
        foreach (UsableItemData i in items)
        {
            if (item == i)
            {
                i.Use(currentPlayer);
            }
        }
        removeAllZeroItem();
    }
}
