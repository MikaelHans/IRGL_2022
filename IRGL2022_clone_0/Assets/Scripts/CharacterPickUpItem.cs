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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in range and "E" is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 direction = fpsCam.transform.forward;
            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, pickUpRange, whatIsItem))
            {
                GameObject item = rayHit.collider.gameObject;                
                pickUpItem(item.GetComponent<Item>());
                photonView.RPC("rpc_destroy", RpcTarget.All, item.GetPhotonView().ViewID);
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
    }
}
