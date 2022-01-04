using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Firebase.Auth;
using Firebase.Firestore;


public class airplane : MonoBehaviour
{
    public GameObject spawnPos;
    public Vector3 movespeed;
    public bool GameStart;
    Camera airplaneCam;
    // Start is called before the first frame update
    void Start()
    {
        GameStart = false;
        airplaneCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameStart == false)
        {
            //player drop from airplane
            PhotonNetwork.Instantiate("Prefabs/First Person Player", spawnPos.transform.position, Quaternion.identity, 0);//instantiate player prefab
            GameStart = true;
            airplaneCam.enabled = false;
            
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position += movespeed * Time.deltaTime;        
    }
}
