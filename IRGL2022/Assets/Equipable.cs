using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipable : Item
{
    public int level = 1;
    public float duralibility = 100;
    public virtual void equip()
    {
        photonView.RPC("equip_RPC", RpcTarget.All);
        photonView.RequestOwnership();
    }

    [PunRPC]
    public void equip_RPC()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
