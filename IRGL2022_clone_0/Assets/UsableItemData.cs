using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemData : ItemData
{
    public void Use(Player user)
    {
        prefab.GetComponent<IUsable>().Use(user);
        amount -= 1;
    }

}
