using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    Cloud cloud;
    void Start()
    {
        cloud = FindObjectOfType<Cloud>();
    }

    public void spawn()
    {
        //random
        int randomIndex = Random.Range(0, cloud.cloud.Count);
        GameObject gobj = PhotonNetwork.Instantiate("Prefabs/" + cloud.cloud[randomIndex].itemName, transform.position, transform.rotation);
        
    }
}
