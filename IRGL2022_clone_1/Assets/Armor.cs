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
       owner.armor_model[level].gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
