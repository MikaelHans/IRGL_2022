using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPoint : MonoBehaviourPun
{
    // Start is called before the first frame update
    public List<Spawn> spawnPoints = new List<Spawn>();

    public void spawnAll()
    {
        List<ItemData> allitems = new List<ItemData>();
        foreach (Spawn spawn in spawnPoints)
        {
            int randomIndex = Random.Range(0, 100);
            if(randomIndex <= 65)
            {
                allitems.Add(spawn.spawn().to_data());
            }            
        }
        string json = JsonHelper.ToJson<ItemData>(allitems.ToArray());
        GameObject chest = PhotonNetwork.InstantiateRoomObject("Prefabs/Chest", transform.position, transform.rotation, 0);
        if (chest != null)
        {
            chest.GetComponent<UnlockableChest>().sync_chest(json);
        }
    }
}
