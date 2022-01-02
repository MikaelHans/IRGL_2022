using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class airplane : MonoBehaviour
{
    public GameObject spawnPos;
    public Vector3 movespeed;
    public bool GameStart;
    // Start is called before the first frame update
    void Start()
    {
        GameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameStart == false)
        {
            //player drop from airplane
            PhotonNetwork.Instantiate("Prefabs/player_", spawnPos.transform.position, Quaternion.identity, 0);//instantiate player prefab
            GameStart = true;
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position += movespeed * Time.deltaTime;        
    }
}
