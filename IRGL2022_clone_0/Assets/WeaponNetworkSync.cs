using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WeaponNetworkSync : MonoBehaviourPun, IPunObservable
{
    public ChracterPickUpWeapon weapons_controller;
    [SerializeField]
    int curr_weapon_in_network, incoming_weapon;

    public WeaponNetworkSync()
    {
        curr_weapon_in_network = -1;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        int curr_weapon;
        if (weapons_controller.gunEquiped != -1)
        {
            curr_weapon = weapons_controller.weapon[weapons_controller.gunEquiped]._weaponID;
        }
        else
        {
            curr_weapon = -1;   
        }
        //Debug.Log("Equipped: " + weapons_controller.gunEquiped+"'\n'Curr:"+curr_weapon);
        
        if (stream.IsWriting)
        {
            stream.SendNext(curr_weapon);
            //Debug.Log( "nopv");
        }
        else
        {
            int equipped_weapon = (int)stream.ReceiveNext();
            incoming_weapon = equipped_weapon;
        }
    }
    private void Update()
    {
        if(!photonView.IsMine)
        {
            if (curr_weapon_in_network != incoming_weapon)
            {
                if (incoming_weapon != -1)
                {
                    if (curr_weapon_in_network != -1)
                    {
                        weapons_controller.GunList[curr_weapon_in_network].gameObject.SetActive(false);
                    }
                    weapons_controller.GunList[incoming_weapon].gameObject.SetActive(true);
                    curr_weapon_in_network = incoming_weapon;
                }
            }
        }        
    }
}
