using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;


public class airplane : MonoBehaviourPun
{
    public GameObject spawnPos;
    public Vector3 movespeed;
    public bool GameStart;
    Camera airplaneCam;
    CinemachineVirtualCamera airplaneVCam;
    Cloud cloud;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GameStart = false;
        airplaneCam = GetComponentInChildren<Camera>();
        airplaneVCam = GetComponentInChildren<CinemachineVirtualCamera>();
        cloud = FindObjectOfType<Cloud>();
        movespeed = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (cloud == null)
        {
            cloud = FindObjectOfType<Cloud>();
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameStart == false)
        {
            //player drop from airplane
            object[] data = new object[1];
            data[0] = cloud.teamID;
            GameObject obj = PhotonNetwork.Instantiate("Prefabs/First Person Player", spawnPos.transform.position, Quaternion.identity, 0, data);//instantiate player prefab            
            obj.name = name;
            GameStart = true;
            airplaneCam.enabled = false;


            // change priority camera
            airplaneVCam.Priority = -100;

            // enable airplane
            // airplaneVCam.Priority = 100;
        }

    }

    private void FixedUpdate()
    {
        gameObject.transform.position += movespeed * Time.deltaTime;
    }
}
