using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Spawn> spawnPoints = new List<Spawn>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnAll()
    {
        foreach(Spawn spawn in spawnPoints)
        {
            spawn.spawn();
        }
    }
}
