using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviourPun
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
    }

    public virtual void removeFromInventory()
    {

    }

    public virtual string getAmmoType()
    {
        return "";
    }
}
