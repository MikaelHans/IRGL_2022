using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName = "item";
    public Sprite icon = null;
    public int maxStack;
    public int amount;
    public GameObject itemObject;
    public GameObject prefab;

    public virtual void Use(Player user)
    {

    }

    public virtual void removeFromInventory()
    {

    }

    public virtual string getAmmoType()
    {
        return "";
    }
}
