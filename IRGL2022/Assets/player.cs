using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class player : MonoBehaviourPun
{
    // Start is called before the first frame update
    Camera cam;
    void Start()
    {        
        cam = gameObject.GetComponentInChildren<Camera>();
        if (!photonView.IsMine)
        {
            cam.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
