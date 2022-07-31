using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameLogic : MonoBehaviourPun
{
    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("Prefabs/Zone", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }
}
