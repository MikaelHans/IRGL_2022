using UnityEngine;
using Photon.Pun;

public abstract class Item : MonoBehaviourPun
{
    public string itemName = "item";
    public Sprite icon = null;
    public int maxStack;
    public int amount;
    public GameObject itemObject;
    public GameObject prefab;
    public int id;
    public Player owner;

    public virtual void Use(Player user)
    {

    }

    public virtual void PickUp(Player player)
    {
        owner = player;
        OnPickup();
    }

    public virtual void removeFromInventory()
    {

    }

    public virtual string getAmmoType()
    {
        return "";
    }

    public virtual void OnPickup()
    {
        photonView.RPC("rpc_destroy", RpcTarget.All);
    }

    [PunRPC]
    public void rpc_destroy()
    {
        if(photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public virtual ItemData to_data()
    {
        ItemData data = new ItemData();
        data.prefab = prefab;
        data.amount = amount;
        data.name = name;

        return data;
    }
}
