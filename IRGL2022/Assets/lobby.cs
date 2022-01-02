using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class lobby : MonoBehaviourPun
{
    public float countdownTime;
    // Start is called before the first frame update
    bool startGame = true;
    void Start()
    {
        PhotonNetwork.Instantiate("Prefabs/First Person Player", new Vector3(0, 2, 0), Quaternion.identity, 0);//instantiate player prefab
                                                                                                   //
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if he is the master client then start countdown timer
        if (PhotonNetwork.IsMasterClient)
        {
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
            }
            else if(startGame)
            {
                //change scene(start game)
                PhotonNetwork.LoadLevel("main");
                startGame = false;
            }
            //Debug.Log(countdownTime);
        }
    }
}
