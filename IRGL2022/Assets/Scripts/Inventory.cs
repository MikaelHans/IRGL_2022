using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviourPun
{
    public InventoryUI UI;

	public List<Item> items = new List<Item>();
    public int spaceLimit = 20;
    public Player currentPlayer;

    public void AddItem(Item item)
    {
        if (items.Count >= spaceLimit)
        {
            Debug.Log("Not enough room.");
            return;
        }

        foreach(Item i in items)
        {
            if(i.GetType().Name == item.GetType().Name && item.itemName == i.itemName)
            {
                int remainingslot = i.maxStack - i.amount;
                int getIn = Mathf.Min(remainingslot, item.amount);
                item.amount -= getIn;
                i.amount += getIn;
            }
        }
        if(item.amount > 0)
        {
            items.Add(item);
        }

        UI.UpdateUI();
    }

    public void Remove(Item item)
    {
        //item.itemObject = Instantiate(item.prefab, transform.position + (transform.forward * 2), transform.rotation);
        item.itemObject = PhotonNetwork.Instantiate("Prefabs/" + item.prefab.name, transform.position + (transform.forward * 2), transform.rotation);
        item.itemObject.GetComponent<Item>().amount = item.amount;
        item.itemObject.GetComponent<Item>().prefab = item.prefab;
        items.Remove(item);
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

    public void UseItemInInventory(Item item)
    {
        foreach (Item i in items)
        {
            if (item == i)
            {
                i.Use(currentPlayer);
            }
        }
        removeAllZeroItem();
    }
}
