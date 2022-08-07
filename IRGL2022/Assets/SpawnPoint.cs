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
                ItemData spawned = spawn.spawn().to_data();
                allitems.Add(spawned);
                if (spawned.prefab.GetComponent<Weapon>() != null)// if weapon spawn also 1 instance of ammo
                {
                    Weapon weapon = spawned.prefab.GetComponent<Weapon>();
                    Cloud cloud = GameObject.FindGameObjectWithTag("cloud").GetComponent<Cloud>();
                    foreach (Item item in cloud.cloud)
                    {
                        if(item.prefab.GetComponent<Ammo>())
                        {
                            if(item.prefab.GetComponent<Ammo>().ammoType == weapon.ammoType)
                            {
                                allitems.Add(item.to_data());
                            }
                        }
                    }
                }
                
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
