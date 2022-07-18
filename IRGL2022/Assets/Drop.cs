using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Drop : MonoBehaviourPun
{
    public Transform dropspot;
    public float dropForce;
    public void DropItem(ItemData item)
    {
        string itemname = item.ItemName;

        GameObject drop = PhotonNetwork.Instantiate("Prefabs/"+itemname,dropspot.position, dropspot.rotation);

        Vector3 force = new Vector3(0, dropForce, dropForce);

        drop.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * dropForce);
    }


}
