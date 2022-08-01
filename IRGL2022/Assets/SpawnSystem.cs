using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnSystem : MonoBehaviour
{
    List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            spawnPoints.AddRange(GetComponentsInChildren<SpawnPoint>());
            spawnAll();
        }       
    }

    public void spawnAll()
    {
        foreach (SpawnPoint spawn in spawnPoints)
        {
            spawn.spawnAll();
        }
    }
}
