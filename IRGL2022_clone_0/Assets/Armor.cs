using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipable
{
    public override void equip()
    {
        base.equip();
        owner.Armor = to_data();
        foreach (GameObject armor in owner.armor_model)
        {
            armor.SetActive(false);
        }
        owner.armor_model[level].gameObject.SetActive(true);

    }
}
