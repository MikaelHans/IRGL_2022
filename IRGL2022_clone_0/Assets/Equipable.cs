using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipable : Item
{
    public int level = 1;
    int seed = 2022;
    public float duralibility = 100;
    public float[] rarity = { 50, 35, 15 };
    public float[] multiplier = { 1, 2, 3 };
    public float defense = 20;
    public Transform[] model;
    private void Awake()
    {
        Transform []transforms = GetComponentsInChildren<Transform>(true);
        model = new Transform[transforms.Length-1];
        for (int i = 0; i < model.Length; i++)
        {
            model[i] = transforms[i+1];            
        }

    }

    protected override void Start()
    {
        base.Start();        
        Random.InitState(seed);
        level = Random.Range(0, 99);
        float start = 0;
        for(int i = 0; i < rarity.Length; i++)
        {
            if(level >= start && level <= start+ rarity[i])
            {
                level = i;
                break;
            }
            start += rarity[i];
        }
        duralibility *= multiplier[level];
        defense *= multiplier[level];
        foreach (Transform obj in model)
        {
            obj.gameObject.SetActive(false);
        }
        model[level].gameObject.SetActive(true);
    }

    public void changeModel(int i)
    {
        
    }

    public virtual void equip()
    {
        photonView.RPC("equip_RPC", RpcTarget.All);
        photonView.RequestOwnership();
    }

    [PunRPC]
    public void equip_RPC()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
