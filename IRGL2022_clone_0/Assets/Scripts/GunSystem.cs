using UnityEngine;
using TMPro;
using Photon.Pun;

public class GunSystem : MonoBehaviourPun
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, ammoCount;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    float finalSpread;
    public float aimAnimationSpeed;
    public float zoomRatio;
    public float defaultFOV;
    public string ammoType;
    public float adsSpread;

    //bools
    bool shooting, readyToShoot, reloading;
    public bool isADS = false;

    //Reference
    public Camera fpsCam, gunCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public CharacterController controller;
    public Inventory inventory;
    //public PickUpController weaponPickUpController;
    public Player currentPlayer;

    //graphics
    public ParticleSystem muzzleFlash;
    public GameObject bulletHoleGraphic, adsContainer, gunContainer;
    public TextMeshProUGUI ammunitionDisplay;
    public GameObject crosshair;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        //PickUpController gunScript = GetComponentInParent<PickUpController>();
        GunSystem gun_system = GetComponent<GunSystem>();

        //gunScript.gunContainer = owner.GetComponent<ChracterPickUpWeapon>().gunContainer.transform;
        //gun_system.fpsCam = owner.GetComponent<ChracterPickUpWeapon>().fpsCam;
        //gun_system.gunCam = owner.GetComponent<ChracterPickUpWeapon>().gunCam;
        //gun_system.inventory = owner.GetComponent<ChracterPickUpWeapon>().inventory;
        //gun_system.controller = owner.GetComponent<ChracterPickUpWeapon>().controller;
        //gun_system.crosshair = owner.GetComponent<ChracterPickUpWeapon>().crosshair;
        //gun_system.ammunitionDisplay = owner.GetComponent<ChracterPickUpWeapon>().ammunitionDisplay;
        //gun_system.currentPlayer = owner;
        GunInit();
    }

    private void Start()
    {
        
    }
    private void Update()
    {     

        MyInput();

        float x = Random.Range(-finalSpread * 1.6f, finalSpread * 1.6f);
        float y = Random.Range(-finalSpread * 0.6f, finalSpread * 0.6f);
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        Debug.DrawLine(fpsCam.transform.position, fpsCam.transform.position + direction*5, Color.red);
    }

    public void GunInit()
    {
        ammoCount = 0;
        foreach (ItemData item in inventory.items)
        {
            if (item.prefab.GetComponent<Ammo>())
            {
                if (item.prefab.GetComponent<Ammo>().getAmmoType() == ammoType)
                {
                    ammoCount += item.amount;
                }
            }
        }
        //Set ammo display if it exist
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + ammoCount);
            ammunitionDisplay.enabled = true;
        }
           
       
    }

    private void MyInput()
    {
        if(currentPlayer.photonView.IsMine)
        {
            //if player can hold mouse to shoot or not(spray opo tapping)
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

            //reload
            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

            //reload if magazine is empty and player try to shoot
            if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
                Reload();

            //shoot
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!fpsCam.GetComponent<MouseLook>().isInventoryOpened)
                {
                    if (!isADS)
                    {
                        ADS();
                    }
                    else
                    {
                        stopADS();
                    }
                }
            }
            if (isADS)
            {
                //transform.position = Vector3.MoveTowards(transform.position, adsContainer.transform.position, aimAnimationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, adsContainer.transform.rotation, aimAnimationSpeed * Time.deltaTime);
                SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, defaultFOV * zoomRatio, aimAnimationSpeed * Time.deltaTime * 2));
            }
            else
            {
                //transform.position = Vector3.MoveTowards(transform.position, gunContainer.transform.position, aimAnimationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, gunContainer.transform.rotation, aimAnimationSpeed * Time.deltaTime);
                SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, defaultFOV, aimAnimationSpeed * Time.deltaTime * 2));
            }
        }       
    }

    public void ADS()
    {
        isADS = true;
        crosshair.SetActive(false);
    }

    public void stopADS()
    {
        isADS = false;
        crosshair.SetActive(true);
    }
    public void cancelADS()
    {
        isADS = false;
        crosshair.SetActive(true);
        transform.position = gunContainer.transform.position;
        transform.rotation = gunContainer.transform.rotation;
        SetFieldOfView(defaultFOV);
    }
    private void SetFieldOfView(float fov)
    {
        fpsCam.fieldOfView = fov;
        gunCam.fieldOfView = fov;
    }
    private void Shoot()
    {
        readyToShoot = false;

        //increase spread when moving
        if (controller.velocity.magnitude > 0)
        {
            finalSpread = spread * 2.0f;
        }
        else
        {
            finalSpread = spread;
        }
        if(isADS)
        {
            finalSpread *= adsSpread;
        }
        //spread
        float x = Random.Range(-finalSpread * 1.6f, finalSpread * 1.6f);
        float y = Random.Range(-finalSpread * 0.6f, finalSpread * 0.6f);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        //if (Physics.Raycast(fpsCam.transform.position + (direction * 0.5f), direction, out rayHit, range, whatIsEnemy)) 
        if (Physics.Raycast(fpsCam.transform.position + (direction * 0.5f), direction, out rayHit, range))
        {            
            if (rayHit.collider.GetComponent<Player>())
            {
                //damage enemy here
                Debug.DrawLine(fpsCam.transform.position, rayHit.point, Color.red, 20.0f, false);
                //Debug.Log(fpsCam.transform.position - rayHit.point);

                float yHitLocation = rayHit.point.y - rayHit.collider.gameObject.transform.position.y;
                Debug.Log(yHitLocation);
                float enemyhealth;
                if(yHitLocation > 0.5)
                {
                    enemyhealth = rayHit.collider.GetComponent<Player>().TakeDamage(damage + damage * 50 / 100, currentPlayer.playerName);
                }
                else if(yHitLocation > 0.2)
                {
                    enemyhealth = rayHit.collider.GetComponent<Player>().TakeDamage(damage + damage * 100 / 100, currentPlayer.playerName);
                }
                else
                {
                    enemyhealth = rayHit.collider.GetComponent<Player>().TakeDamage(damage - damage * 20 / 100, currentPlayer.playerName);
                }  
                if(enemyhealth <= 0)
                {
                    //killed an enemy then add points
                    currentPlayer.points += 100;
                }
            }
            if (rayHit.collider.CompareTag("Wall"))
            {
                Debug.DrawLine(fpsCam.transform.position, rayHit.point, Color.green, 20.0f, false);
                Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }

        }

        //shake camera

        //graphics        
        photonView.RPC("rpz_muzzle_flash", RpcTarget.All);

        bulletsLeft--;
        bulletsShot--;
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + ammoCount);

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

    }

    [PunRPC]
    public void rpz_muzzle_flash()
    {
        muzzleFlash.Play();
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {          
        //int bulletreload = Mathf.Min(ammoCount * bulletsPerTap, magazineSize);
        //bulletsLeft = bulletreload;
        int missingBullet = (magazineSize - bulletsLeft) / bulletsPerTap;
        foreach (ItemData item in inventory.items)
        {
            if(item.prefab.GetComponent<Ammo>())
            {
                if(item.prefab.GetComponent<Ammo>().getAmmoType() == ammoType)
                {
                    int bulletremove = Mathf.Min(missingBullet, item.amount);
                    missingBullet -= bulletremove;
                    item.amount -= bulletremove;
                    bulletsLeft += bulletremove * bulletsPerTap;
                }
            }
        }

        ammoCount = 0;
        
        foreach (ItemData item in inventory.items)
        {
            if (item.prefab.GetComponent<Ammo>())
            {
                if (item.prefab.GetComponent<Ammo>().getAmmoType() == ammoType)
                {
                    ammoCount += item.amount;
                }
            }
        }

        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + ammoCount);
        inventory.removeAllZeroItem();
        reloading = false;
    }
}
