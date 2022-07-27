using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMinigame : MonoBehaviour
{
    public RaycastHit rayHit;
    public LayerMask whatIsItem;
    public Camera fpsCam;
    public float pickUpRange;
    public Player player;
    // Start is called before the first frame update

    bool minigameKeyPressed = false;
    public void Minigame()
    {
        minigameKeyPressed = true;
    }
    public void ResetKeys()
    {
        minigameKeyPressed = false;
    }
    void Start()
    {

    }
    void Update()
    {
        //Check if player is in range and "E" is pressed
        if (minigameKeyPressed)
        {
            Vector3 direction = fpsCam.transform.forward;
            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, pickUpRange, whatIsItem))
            {
                GameObject chest = rayHit.collider.gameObject.transform.parent.gameObject;
                chest.GetComponent<UnlockableChest>().Open(player);
            }
        }

        ResetKeys();
    }
}
