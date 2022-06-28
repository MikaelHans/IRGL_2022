using Photon.Pun;
using UnityEngine;

public class CharacterPickUpItem : MonoBehaviourPun
{
    public Inventory inventory;
    public RaycastHit rayHit;
    public LayerMask whatIsItem;
    public Camera fpsCam;
    public float pickUpRange;
    // Start is called before the first frame update
    public struct ItemData
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in range and "E" is pressed
        if (Input.GetKeyDown(KeyCode.E) && photonView.IsMine)
        {
            Vector3 direction = fpsCam.transform.forward;
            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, pickUpRange, whatIsItem))
            {
                GameObject item = rayHit.collider.gameObject;
                item.GetComponent<Item>().PickUp(gameObject.GetComponent<Player>());
                if(item.GetComponent<IStoreable>() != null)
                {
                    pickUpItem(item.GetComponent<Item>());
                    photonView.RPC("rpc_destroy", RpcTarget.All, item.GetPhotonView().ViewID);
                }    
                else if(item.GetComponent<Equipable>())
                {
                    item.GetComponent<Equipable>().equip();
                }
            }
        }
    }

    [PunRPC]
    public void rpc_destroy(int viewID)
    {
        GameObject item = PhotonView.Find(viewID).gameObject;
        if (item.GetPhotonView().IsMine)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID));
        }        
    }

    void pickUpItem(Item item)
    {
        inventory.AddItem(item);     
        if(item.GetComponent<Ammo>())
        {
            ChracterPickUpWeapon weaponInventory = gameObject.GetComponent<ChracterPickUpWeapon>();
            foreach (GameObject gun in weaponInventory.weapon)
            {
                if (gun != null && gun.GetComponent<GunSystem>())
                {
                    gun.GetComponent<GunSystem>().GunInit();
                }                
            }
        }
    }
}
