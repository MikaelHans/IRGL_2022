using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class AdminMonitor : MonoBehaviourPunCallbacks
{
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        //base.OnMasterClientSwitched(newMasterClient);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
        //Application.Quit();
    }
}
