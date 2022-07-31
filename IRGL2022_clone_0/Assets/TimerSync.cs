using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TimerSync : MonoBehaviourPun, IPunObservable
{
    ShrinkLogic shrinkLogic;
    float currTime;

    private void Awake()
    {
        shrinkLogic = GetComponent<ShrinkLogic>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(shrinkLogic.totalTime);
            //Debug.Log( "nopv");
        }
        else
        {
            currTime = (float)stream.ReceiveNext();            
        }
    }
    void Update()
    {
        if(!photonView.IsMine)
        {
            shrinkLogic.totalTime = currTime;
        }
    }
}
