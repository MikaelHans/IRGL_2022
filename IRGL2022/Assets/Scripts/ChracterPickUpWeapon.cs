using TMPro;
using UnityEngine;

public class ChracterPickUpWeapon : MonoBehaviour
{
    public bool slotFull;
    public RaycastHit rayHit;
    public LayerMask whatIsgun;
    public float pickUpRange;
    public GameObject[] weapon = new GameObject[3];
    public GameObject gunContainer;
    public GameObject adsContainer;
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
        if(gunEquiped < 0 || gunEquiped >= 2)
        {
            ammunitionDisplay.enabled = false;
        }
        else
        {
            if (weapon[gunEquiped] == null)
            {
                ammunitionDisplay.enabled = false;
            }
            else
            {
                ammunitionDisplay.enabled = true;
            }
        }
        

        //Check if player is in range and "E" is pressed
        if(Input.GetKeyDown(KeyCode.E))
        {
            Vector3 direction = fpsCam.transform.forward;

            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, pickUpRange, whatIsgun))
            {
                GameObject gun = rayHit.collider.gameObject;
                PickUpController gunScript = gun.GetComponent<PickUpController>();
                GunSystem gun_system = gun.GetComponent<GunSystem>();
                if(!gunScript.equipped && !slotFull)
                {
                    gunScript.gunContainer = gunContainer.transform;
                    gunScript.adsContainer = adsContainer.transform;
                    gunScript.fpsCam = fpsCam.transform;
                    gunScript.player = gameObject.transform;
                    gun_system.fpsCam = fpsCam;
                    gun_system.gunCam = gunCam;
                    gun_system.inventory = inventory;
                    gun_system.controller = controller;
                    gun_system.crosshair = crosshair;
                    gun_system.ammunitionDisplay = ammunitionDisplay;
                    gun_system.currentPlayer = currentPlayer;
                    gunScript.PickUp();
                    moveToSlot(gun);
                    updateSlot();
                }
            }
        }

        //Drop if equipped and "Q" is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(gunEquiped != 2)
                dropgunFromSlot(gunEquiped);
        }

        if(Input.mouseScrollDelta.y > 0.0f)
        {
            int i = getPrevWeapon();
            if(i >= 0 && i < weapon.Length)
            {
                if (weapon[gunEquiped] != null)
                {
                    if (weapon[gunEquiped].GetComponent<GunSystem>() != null)
                        weapon[gunEquiped].GetComponent<GunSystem>().stopADS();
                    weapon[gunEquiped].transform.position = gunContainer.transform.position;
                }
                gunEquiped = i;
                setActiveGun();
            }
        }
        if (Input.mouseScrollDelta.y < 0.0f)
        {
            int i = getNextWeapon();
            if (i >= 0 && i < weapon.Length)
            {
                if(weapon[gunEquiped] != null)
                {
                    if (weapon[gunEquiped].GetComponent<GunSystem>() != null)
                        weapon[gunEquiped].GetComponent<GunSystem>().stopADS();
                    weapon[gunEquiped].transform.position = gunContainer.transform.position;
                }                
                gunEquiped = i;
                setActiveGun();
            }
        }
    }

    private int getNextWeapon()
    {
        for(int i = (gunEquiped + 1) % weapon.Length; i != gunEquiped; i = (i + 1) % weapon.Length)
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
                i = weapon.Length - 1;
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
        for(int i = 0;i< weapon.Length;i++)
        {
            if(weapon[i] == null)
            {
                slotFull = false;
                return;
            }
        }
        slotFull = true;
    }

    private void moveToSlot(GameObject gun)
    {
        for(int i = 0; i < weapon.Length; i++)
        {
            if(weapon[i] == null)
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
        for(int i = 0; i < weapon.Length; i++)
        {
            if(i == gunEquiped && weapon[i] != null)
            {
                weapon[i].SetActive(true);
            }
            else if(weapon[i])
            {
                weapon[i].SetActive(false);
            }
        }
    }

    private void dropgunFromSlot(int index)
    {
        if(index >=0 && index < weapon.Length)
        {
            if(weapon[index] != null)
            {
                weapon[index].SetActive(true);
                weapon[index].GetComponent<GunSystem>().cancelADS();
                weapon[index].GetComponent<PickUpController>().Drop();
                weapon[index] = null;
                updateSlot();
            }
        }
    }
}
