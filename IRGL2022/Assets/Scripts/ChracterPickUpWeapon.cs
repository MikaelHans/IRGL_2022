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
    public GameObject meleesystem;

    public Drop dropsystem;

    bool pickupKeyPressed = false;
    bool dropKeyPressed = false;

    float scrollDelta = 0;

    // Start is called before the first frame update
    void Start()
    {
        gunEquiped = -1;
        meleesystem = GetComponentInChildren<MeleeSystem>(true).gameObject;
    }

    public void Pickup()
    {
        pickupKeyPressed = true;
    }

    public void Drop()
    {
        dropKeyPressed = true;
    }

    public void Scroll(float value)
    {
        scrollDelta = value;
    }

    public void ResetKeys()
    {
        pickupKeyPressed = false;
        dropKeyPressed = false;
        scrollDelta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            #region old codes
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
            #endregion
            //Check if player is in range and "E" is pressed
            if (pickupKeyPressed)
            {
                Vector3 direction = fpsCam.transform.forward;
                //RayCast
                if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, pickUpRange))
                {
                    GameObject gun = rayHit.collider.gameObject;
                    if (gun.GetComponentInParent<Weapon>() && !slotFull)
                    {
                        Weapon wpn = gun.GetComponentInParent<Weapon>();
                        WeaponData weapondata = new WeaponData();
                        weapondata.prefab = wpn.prefab;
                        weapondata.Name = wpn.itemName;
                        weapondata.amount = wpn.amount;
                        weapondata._gunsystem = GunList[wpn.WeaponID];
                        weapondata._weaponID = wpn.WeaponID;
                        wpn.PickUp(currentPlayer);
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
            }

            //Drop if equipped and "Q" is pressed
            if (dropKeyPressed)
            {
                if (gunEquiped != 2)
                    dropgunFromSlot(gunEquiped);
            }

            if (scrollDelta > 0.0f)
            {
                ammunitionDisplay.text = "";
                int i = getPrevWeapon();
                if (i >= 0 && i < weapon.Count)
                {
                    if (weapon[i]._gunsystem != null)
                    {
                        if (weapon[i]._gunsystem != null)
                            weapon[i]._gunsystem.stopADS();
                        //weapon[gunEquiped].transform.position = gunContainer.transform.position;
                        weapon[i]._gunsystem.gameObject.SetActive(true);
                    }
                    gunEquiped = i;
                    setActiveGun(gunEquiped);
                }
                else if (i == -1)
                {
                    currentPlayer.animator.SetBool("IsCarryingAWeapon", false);
                    meleesystem.SetActive(true);
                    if (gunEquiped != -1)
                    {
                        weapon[gunEquiped]._gunsystem.gameObject.SetActive(false);
                    }
                    gunEquiped = i;
                }
                //photonView.RPC("rpc_pickup", RpcTarget.All, weapon[gunEquiped]);
            }
            if (scrollDelta < 0.0f)
            {
                ammunitionDisplay.text = "";
                int i = getNextWeapon();
                if (i >= 0 && i < weapon.Count)
                {
                    if (weapon[i]._gunsystem != null)
                    {
                        if (weapon[i]._gunsystem != null)
                            weapon[i]._gunsystem.stopADS();
                        //weapon[gunEquiped].transform.position = gunContainer.transform.position;
                        weapon[i]._gunsystem.gameObject.SetActive(true);
                    }
                    gunEquiped = i;
                    setActiveGun(gunEquiped);
                }
                else if (i == -1)
                {
                    currentPlayer.animator.SetBool("IsCarryingAWeapon", false);
                    meleesystem.SetActive(true);
                    if (gunEquiped != -1)
                    {
                        weapon[gunEquiped]._gunsystem.gameObject.SetActive(false);
                    }
                    gunEquiped = i;
                }
            }

            ResetKeys();
        }
    }

    [PunRPC]
    public void rpc_equip_weapon(int weaponID)
    {
        if (!photonView.IsMine)
        {
            GunList[weaponID].gameObject.SetActive(true);
        }
    }

    [PunRPC]
    public void rpc_pickup(Vector3 direction)
    {

    }

    private int getNextWeapon()
    {
        if (gunEquiped == -1)
        {
            for (int i = 0; i < weapon.Count; i++)
            {
                if (weapon[i]._gunsystem != null)
                {
                    return i;
                }
            }
            return -1;
        }
        else
        {
            for (int i = (gunEquiped + 1) % weapon.Count; i != gunEquiped; i = (i + 1) % weapon.Count)
            {
                if (weapon[i]._gunsystem != null)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    private int getPrevWeapon()
    {
        if (gunEquiped == -1)
        {
            for (int i = 0; i < weapon.Count; i++)
            {
                if (weapon[i]._gunsystem != null)
                {
                    return i;
                }
            }
            return -1;
        }
        else
        {
            for (int i = (gunEquiped - 1); i != gunEquiped; i--)
            {
                if (i < 0)
                {
                    i = weapon.Count - 1;
                }
                if (weapon[i]._gunsystem != null)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    private void updateSlot()
    {
        for (int i = 0; i < weapon.Count; i++)
        {
            if (weapon[i]._gunsystem == null)
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
        currentPlayer.animator.SetBool("IsCarryingAWeapon", true);
        meleesystem.SetActive(false);
        for (int i = 0; i < weapon.Count; i++)
        {
            if (weapon[i]._gunsystem == null)
            {
                weapon[i] = gun;
                gunEquiped = i;
                setActiveGun(gunEquiped);
                break;
            }
        }
    }

    private void setActiveGun(int _gunEquiped)
    {
        currentPlayer.animator.SetBool("IsCarryingAWeapon", true);
        meleesystem.SetActive(false);
        for (int i = 0; i < weapon.Count; i++)
        {
            if (i == _gunEquiped && weapon[i]._gunsystem != null)
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
                //weapon[i]._gunsystem.ammunitionDisplay.enabled = false;
                weapon[i]._gunsystem.gameObject.SetActive(false);
            }

        }
        inventory.UI.UpdateUI();
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
                //weapon[index]._gunsystem.gameObject.SetActive(false);
                //set all gameobject to false to hide gun model
                for (int i = 0; i < weapon.Count; i++)
                {
                    GunList[i].gameObject.SetActive(false);
                }
                //cancel ads
                weapon[index]._gunsystem.gameObject.SetActive(false);
                weapon[index]._gunsystem.cancelADS();
                weapon[index]._gunsystem.ammunitionDisplay.enabled = false;
                //PhotonNetwork.Instantiate("Prefabs/" + weapon[index].Name, transform.position, transform.rotation);
                //drop item
                dropsystem.DropItem(weapon[index],1);
                //overwrite item data to null
                weapon[index] = new WeaponData();
                //update slot
                updateSlot();
                //set gun equipped status to -1
                gunEquiped = -1;
                //set animation
                currentPlayer.animator.SetBool("IsCarryingAWeapon", false);
                //enable melee system
                meleesystem.SetActive(true);
            }
        }
    }
}
