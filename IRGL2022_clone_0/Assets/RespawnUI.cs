using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject uiParent;

    private void Start()
    {
        //canvas = GetComponentInChildren<Canvas>();
    }
    public bool checkIfAirplaneHasSpawned()
    {
        airplane _airplane = FindObjectOfType<airplane>();
        if(_airplane != null)
        {
            return true;
        }
        return false;
    }

    public bool checkIfPlayerHasSpawned()
    {
        List<Player> allplayer = new List<Player>(FindObjectsOfType<Player>());

        foreach (Player player in allplayer)
        {
            if (player.photonView.IsMine)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if(checkIfAirplaneHasSpawned())
        {
            uiParent.SetActive(false);
        }
        else
        {
            if(!checkIfPlayerHasSpawned() && uiParent != null)
            {
                uiParent.SetActive(true);
            }                
        }
    }
}
