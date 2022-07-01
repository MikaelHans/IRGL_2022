using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemData : ItemData
{
    float healthRestored = 25.0f;
    public void Use(Player user)
    {

        if (user.currentHealth < user.maxHealth)
        {
            user.RecoverHealth(healthRestored);
            amount -= 1;
        }
    }

}
