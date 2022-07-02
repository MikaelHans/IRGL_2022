using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EquippedNetworkSync : MonoBehaviourPun, IPunObservable
{
    public Player player;
    [SerializeField]
    int curr_helmet_in_network, incoming_helmet , curr_armor_in_network, incoming_armor;

    public EquippedNetworkSync()
    {
        curr_helmet_in_network = -1;
        curr_armor_in_network = -1;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        int curr_armor, curr_helmet;
        if (player.Armor.prefab != null)
        {
            curr_armor = player.Armor.prefab.GetComponent<Equipable>().level;
        }
        else
        {
            curr_armor = -1;
        }
        if (player.Helmet.prefab != null)
        {
            curr_helmet = player.Helmet.prefab.GetComponent<Equipable>().level;
        }
        else
        {
            curr_helmet = -1;
        }

        if (stream.IsWriting)
        {
            stream.SendNext(curr_armor);
            stream.SendNext(curr_helmet);
            //Debug.Log( "nopv");
        }
        else
        {
            incoming_armor = (int)stream.ReceiveNext();
            incoming_helmet = (int)stream.ReceiveNext(); 
        }
    }
    private void Update()
    {
        if (!photonView.IsMine)
        {
            if (curr_helmet_in_network != incoming_helmet)
            {
                if (incoming_helmet != -1)
                {
                    foreach (GameObject helmet in player.helmet_model)
                    {
                        helmet.SetActive(false);
                    }
                    player.helmet_model[incoming_helmet].gameObject.SetActive(true);
                    curr_helmet_in_network = incoming_helmet;
                }
            }

            if (curr_armor_in_network != incoming_armor)
            {
                if (incoming_armor != -1)
                {
                    foreach (GameObject armor in player.armor_model)
                    {
                        armor.SetActive(false);
                    }
                    player.armor_model[incoming_armor].gameObject.SetActive(true);
                    curr_armor_in_network = incoming_armor;
                }
            }
        }
    }
}
