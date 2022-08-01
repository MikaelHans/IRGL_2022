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
    public Vector3 destination;
    bool has_dropped;
    // Start is called before the first frame update
    void Start()
    {
        has_dropped = false;
        GameStart = false;
        airplaneCam = GetComponentInChildren<Camera>();
        airplaneVCam = GetComponentInChildren<CinemachineVirtualCamera>();
        cloud = FindObjectOfType<Cloud>();
        movespeed = transform.forward * speed;

        if (checkIfPlayerHasSpawned())
        {
            airplaneVCam.Priority = -100;
        }
    }

    public bool checkIfPlayerHasSpawned()
    {
        List<Player> allplayer = new List<Player>(FindObjectsOfType<Player>());

        foreach (Player player in allplayer)
        {
            if(player.photonView.IsMine)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cloud == null)
        {
            cloud = FindObjectOfType<Cloud>();
        }
        if (checkIfPlayerHasSpawned())
        {
            airplaneVCam.Priority = -100;
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameStart == false && !checkIfPlayerHasSpawned())
        {
            //player drop from airplane
            has_dropped=true;
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
        //Debug.Log(Vector3.Distance(transform.position, destination));
        if (Vector3.Distance(transform.position, destination) <= 1f)
        {
            if (!checkIfPlayerHasSpawned())
            {
                has_dropped = true;
                object[] data = new object[1];
                data[0] = cloud.teamID;
                GameObject obj = PhotonNetwork.Instantiate("Prefabs/First Person Player", spawnPos.transform.position, Quaternion.identity, 0, data);//instantiate player prefab            
                obj.name = name;
                GameStart = true;
                airplaneCam.enabled = false;
                // change priority camera
                airplaneVCam.Priority = -100;
            }
            if (PhotonNetwork.IsMasterClient)
            {                
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    public void destroy()
    {

    }

    private void FixedUpdate()
    {
        if(photonView.IsMine)
            gameObject.transform.position += transform.forward * speed * Time.deltaTime;
    }
}
