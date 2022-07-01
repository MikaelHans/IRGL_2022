using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Helmet : Equipable
{
    // Start is called before the first frame update
    public override void equip()
    {
        base.equip();   
        owner.Helmet = to_data();
        owner.helmet_model[level].gameObject.SetActive(true);
    }

}
