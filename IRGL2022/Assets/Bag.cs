using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Equipable
{
    public override void equip()
    {
        base.equip();
        owner.bag_model[level].gameObject.SetActive(true);
        owner.inventory.InitInventory(level);
    }  
}
