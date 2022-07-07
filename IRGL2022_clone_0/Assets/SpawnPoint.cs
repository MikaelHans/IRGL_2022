using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Spawn> spawnPoints = new List<Spawn>();

    public void spawnAll()
    {
        foreach(Spawn spawn in spawnPoints)
        {
            int randomIndex = Random.Range(0, 100);
            if(randomIndex <= 50)
            {
                spawn.spawn();
            }            
        }
    }
}
