using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Drop : MonoBehaviourPun
{
    public Transform dropspot;
    public void DropItem(ItemData item)
    {
        string itemname = item.ItemName;

        GameObject drop = PhotonNetwork.Instantiate(itemname,dropspot.position, dropspot.rotation);
    }


}
