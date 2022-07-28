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
        cloud = GameObject.FindGameObjectWithTag("cloud").GetComponent<Cloud>();
    }

    private void Update()
    {
        if(cloud == null)
        {
            cloud = GameObject.FindGameObjectWithTag("cloud").GetComponent<Cloud>();
        }
    }

    public void spawn()
    {
        //random
        if(cloud == null)
        {
            cloud = GameObject.FindGameObjectWithTag("cloud").GetComponent<Cloud>();
        }
        int randomIndex = Random.Range(0, cloud.cloud.Count);
        GameObject gobj = PhotonNetwork.Instantiate("Prefabs/" + cloud.cloud[randomIndex].itemName, transform.position, transform.rotation);
    }
    
}
