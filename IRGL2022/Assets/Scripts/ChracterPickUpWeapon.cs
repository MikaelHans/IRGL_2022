using TMPro;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class ChracterPickUpWeapon : MonoBehaviourPun
{
    public bool slotFull;
    public RaycastHit rayHit;
    public LayerMask whatIsgun;
    public float pickUpRange;
    public List<WeaponData> weapon = new List<WeaponData>(3);
    public GameObject gunContainer;
    public GameObject adsContainer;
    public List<GunSystem> GunList = new List<GunSystem>();
    public Transform player;
    public Camera fpsCam;
    public int gunEquiped;
    public Camera gunCam;
    public CharacterController controller;
    public Inventory inventory;
    public TextMeshProUGUI ammunitionDisplay;
    public GameObject crosshair;
    public Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        gunEquiped = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            //if (gunEquiped < 0 || gunEquiped >= 2)
            //{
            //    ammunitionDisplay.enabled = false;
            //}
            //else
            //{
            //    if (weapon[gunEquiped] == null)
            //    {
            //        ammunitionDisplay.enabled = false;
            //    }
            //    else
            //    {
            //        ammunitionDisplay.enabled = true;
            //    }
            //}


            //Check if player is in range and "E" is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 direction = fpsCam.transform.forward;
                //RayCast
                if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, pickUpRange))
                {
                    GameObject gun = rayHit.collider.gameObject;
                    if(gun.GetComponentInParent<Weapon>())
                    {
                        Weapon wpn = gun.GetComponentInParent<Weapon>();
                        WeaponData weapondata = new WeaponData();
                        weapondata.prefab = wpn.prefab;
                        weapondata.name = wpn.itemName;
                        weapondata.amount = wpn.amount;
                        weapondata._gunsystem = GunList[wpn.WeaponID];

                        wpn.photonView.RequestOwnership();
                        PhotonNetwork.Destroy(wpn.gameObject);
                        moveToSlot(weapondata);
                        updateSlot();
                    }
                    #region old codes
                    //PickUpController gunScript = GetComponent<PickUpController>();
                    //GunSystem gun_system = GetComponent<GunSystem>();

                    //if (!gunScript.equipped && !slotFull)
                    //{
                    //    gunScript.gunContainer = gunContainer.transform;
                    //    gunScript.adsContainer = adsContainer.transform;
                    //    gunScript.fpsCam = fpsCam.transform;
                    //    gunScript.player = gameObject.transform;
                    //    gun_system.fpsCam = fpsCam;
                    //    gun_system.gunCam = gunCam;
                    //    gun_system.inventory = inventory;
                    //    gun_system.controller = controller;
                    //    gun_system.crosshair = crosshair;
                    //    gun_system.ammunitionDisplay = ammunitionDisplay;
                    //    gun_system.currentPlayer = currentPlayer;
                    //    gun_system.GunInit();
                    //    gunScript.PickUp();
                    //    moveToSlot(gun);
                    //    updateSlot();
                    //}
                    #endregion
                    
                }

                //photonView.RPC("rpc_pickup", RpcTarget.All, direction);
            }

            //Drop if equipped and "Q" is pressed
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (gunEquiped != 2)
                    dropgunFromSlot(gunEquiped);
            }

            if (Input.mouseScrollDelta.y > 0.0f)
            {
                int i = getPrevWeapon();
                if (i >= 0 && i < weapon.Count)
                {
                    if (weapon[gunEquiped] != null)
                    {
                        if (weapon[gunEquiped]._gunsystem != null)
                            weapon[gunEquiped]._gunsystem.stopADS();
                        //weapon[gunEquiped].transform.position = gunContainer.transform.position;
                        weapon[gunEquiped]._gunsystem.gameObject.SetActive(true);
                    }
                    gunEquiped = i;
                    setActiveGun();
                }
                //photonView.RPC("rpc_pickup", RpcTarget.All, weapon[gunEquiped]);
            }
            if (Input.mouseScrollDelta.y < 0.0f)
            {
                int i = getNextWeapon();
                if (i >= 0 && i < weapon.Count)
                {
                    if (weapon[gunEquiped] != null)
                    {
                        if (weapon[gunEquiped]._gunsystem != null)
                            weapon[gunEquiped]._gunsystem.stopADS();
                        //weapon[gunEquiped].transform.position = gunContainer.transform.position;
                        weapon[gunEquiped]._gunsystem.gameObject.SetActive(true);
                    }
                    gunEquiped = i;
                    setActiveGun();
                }
                //photonView.RPC("rpc_pickup", RpcTarget.All, weapon[gunEquiped]);
            }
        }        
    }

    [PunRPC]
    public void rpc_changeWeapon(int weaponID)
    {
        if(!photonView.IsMine)
        {
            
        }
    }

    [PunRPC]
    public void rpc_pickup(Vector3 direction)
    {
        
    }

    private int getNextWeapon()
    {
        for(int i = (gunEquiped + 1) % weapon.Count; i != gunEquiped; i = (i + 1) % weapon.Count)
        {
            if(weapon[i] != null)
            {
                return i;
            }
        }
        return -1;
    }

    private int getPrevWeapon()
    {
        for (int i = (gunEquiped - 1); i != gunEquiped; i--)
        {
            if(i < 0)
            {
                i = weapon.Count - 1;
            }
            if (weapon[i] != null)
            {
                return i;
            }
        }
        return -1;
    }

    private void updateSlot()
    {
        for(int i = 0;i< weapon.Count; i++)
        {
            if(weapon[i]._gunsystem == null)
            {
                slotFull = false;
                return;
            }
        }
        slotFull = true;
    }

    private void moveToSlot(WeaponData gun)
    {
        //weapon[gunEquiped]._gunsystem.gameObject.SetActive(false);
        for(int i = 0; i < weapon.Count; i++)
        {
            if(weapon[i]._gunsystem == null)
            {
                weapon[i] = gun;
                gunEquiped = i;
                setActiveGun();
                break;
            }
        }
    }

    private void setActiveGun()
    {
        for(int i = 0; i < weapon.Count; i++)
        {
            if(i == gunEquiped && weapon[i]._gunsystem != null)
            {
                weapon[i]._gunsystem.gameObject.SetActive(true);
                weapon[i]._gunsystem.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                if (weapon[i]._gunsystem.gameObject.GetComponent<GunSystem>())
                {
                    weapon[i]._gunsystem.gameObject.GetComponent<GunSystem>().GunInit();  
                }
            }
            else if (weapon[i]._gunsystem)
            {
                weapon[i]._gunsystem.ammunitionDisplay.enabled = false;
                weapon[i]._gunsystem.gameObject.SetActive(false);
            }

        }
    }

    public void dropgunFromSlot(int index)
    {
        photonView.RPC("rpc_drop", RpcTarget.All, index);
    }

    public WeaponData[] dropgunAllGun()
    {
        return weapon.ToArray();
    }

    [PunRPC]
    public void rpc_drop(int index)
    {
        if (index >= 0 && index < weapon.Count)
        {
            if (weapon[index]._gunsystem != null)
            {
                weapon[index]._gunsystem.gameObject.SetActive(false);
                weapon[index]._gunsystem.cancelADS();
                weapon[index]._gunsystem.ammunitionDisplay.enabled = false;
                PhotonNetwork.Instantiate("Prefabs/" + weapon[index].name, transform.position,transform.rotation);
                weapon[index] = new WeaponData();
                updateSlot();
            }
        }
    }
}
