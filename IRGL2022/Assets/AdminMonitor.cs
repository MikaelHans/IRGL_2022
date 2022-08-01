using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AdminMonitor : MonoBehaviourPunCallbacks
{
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        //base.OnMasterClientSwitched(newMasterClient);
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
