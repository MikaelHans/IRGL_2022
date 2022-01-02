using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableChest : MonoBehaviourPun
{
    public bool isPlayed = false, isOpened = false;
    public GameObject[] items;
    public Minigame game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(Player player)
    {
        if(!isPlayed && !isOpened)
        {
            isPlayed = true;
            game.Init();
            game.Play(player);
        }
    }

    public void dropContent()
    {
        isOpened = true;
        for(int i = 0; i < items.Length; i++)
        {
            PhotonNetwork.Instantiate("Prefabs/" + items[i].name, transform.position + (transform.forward * 2), transform.rotation);
        }
        game.closeWindow();
        Destroy(gameObject);
    }

    public void Close()
    {
        isPlayed = false;
    }
}
