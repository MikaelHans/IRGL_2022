using Photon.Pun;
using TMPro;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, range, timeBetweenShots;
    public int bulletsPerTap;

    //bools
    bool shooting, readyToShoot;

    //Reference
    public Camera fpsCam, gunCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public CharacterController controller;
    public Player currentPlayer;

    //graphics
    public TextMeshProUGUI ammunitionDisplay;
    public GameObject crosshair;

    private void Awake()
    {
        readyToShoot = true;
    }
    private void Update()
    {
        ammunitionDisplay.text = "";
        MyInput();
    }

    private void MyInput()
    {
        if(currentPlayer.photonView.IsMine)
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

            //shoot
            if (readyToShoot && shooting)
            {
                Shoot();
            }
        }        
    }

    private void Shoot()
    {
        readyToShoot = false;


        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward;

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position + (direction * 0.5f), direction, out rayHit, range, whatIsEnemy))
        {
            float enemyhealth;
            if (rayHit.collider.CompareTag("Enemy"))
            {
                //damage enemy here
                enemyhealth = rayHit.collider.GetComponent<Player>().TakeDamage(damage, currentPlayer.playerName);
                if (enemyhealth <= 0)
                {
                    //killed an enemy then add points
                    currentPlayer.points += 200;
                }

            }
            

        }

        //shake camera

        //graphics        

        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    
}
