using UnityEngine;

public class CharacterPickUpItem : MonoBehaviour
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
                Destroy(item);                
            }
        }
    }

    void pickUpItem(Item item)
    {
        inventory.AddItem(item);        
    }
}
