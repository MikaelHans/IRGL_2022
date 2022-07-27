using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableChest : MonoBehaviourPun
{
    public bool isPlayed = false, isOpened = false;


    public ItemData[] items;
    public Minigame game;

    public void Open(Player player)
    {
        if(!isPlayed && !isOpened)
        {
            isPlayed = true;
            game.Init();
            game.Play(player);
        }
    }

    public void dropContent()
    {
        isOpened = true;
        for(int i = 0; i < items.Length; i++)
        {
            GameObject tmp = PhotonNetwork.Instantiate("Prefabs/" + items[i].prefab.GetComponent<Item>().itemName, transform.position + (transform.forward * 2), transform.rotation);
            tmp.GetComponent<Item>().prefab = items[i].prefab;
            tmp.GetComponent<Item>().amount = items[i].amount;
            tmp.GetComponent<Item>().name = items[i].Name;
        }
        game.closeWindow();
        destroyChest();
    }
    
    public void fillChest(ItemData[] _items)
    {
        items = _items;
    }

    public void destroyChest()
    {
        photonView.RPC("destroyChestRPC", RpcTarget.All, gameObject.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void destroyChestRPC(int viewID)
    {
        GameObject item = PhotonView.Find(viewID).gameObject;
        if (item.GetPhotonView().IsMine)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID));
        }
    }

    public void sync_chest(string json)
    {
        //rpc call
        photonView.RPC("sync_chest_RPC", RpcTarget.All, json);
    }

    [PunRPC]
    public void sync_chest_RPC(string json)
    {
        ItemData[] incomingItemData = JsonHelper.FromJson<ItemData>(json);
        items = incomingItemData;
        for (int i = 0; i < incomingItemData.Length; i++)
        {
            string itemname = incomingItemData[i].Name;
            Cloud cloud = FindObjectOfType<Cloud>();
            foreach (Item prefab in cloud.cloud)
            {
                if (prefab.itemName == itemname)
                {
                    items[i].prefab = prefab.GetComponent<Item>().prefab;
                }
            }
        }
    }

    public void Close()
    {
        isPlayed = false;
    }
}
