using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
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
    public PickUpController weaponPickUpController;
    public Player currentPlayer;

    //graphics
    public ParticleSystem muzzleFlash;
    public GameObject bulletHoleGraphic;
    public TextMeshProUGUI ammunitionDisplay;
    public GameObject crosshair;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {       

        MyInput();
        int ammoCount = 0;
        foreach (Item item in inventory.items)
        {
            if (item.GetType().Name == "Ammo")
            {
                if (item.getAmmoType() == ammoType)
                {
                    ammoCount += item.amount;
                }
            }
        }
        //Set ammo display if it exist
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + ammoCount);
    }

    private void MyInput()
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

        if(Input.GetMouseButtonDown(1))
        {
            if(!fpsCam.GetComponent<MouseLook>().isInventoryOpened)
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
        if(isADS)
        {
            transform.position = Vector3.MoveTowards(transform.position, weaponPickUpController.adsContainer.position, aimAnimationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, weaponPickUpController.adsContainer.transform.rotation, aimAnimationSpeed * Time.deltaTime);
            SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, defaultFOV * zoomRatio, aimAnimationSpeed * Time.deltaTime * 2));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, weaponPickUpController.gunContainer.position, aimAnimationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, weaponPickUpController.gunContainer.transform.rotation, aimAnimationSpeed * Time.deltaTime);
            SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, defaultFOV, aimAnimationSpeed * Time.deltaTime * 2));
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
        transform.position = weaponPickUpController.gunContainer.position;
        transform.rotation = weaponPickUpController.gunContainer.transform.rotation;
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
        if (Physics.Raycast(fpsCam.transform.position + (direction * 0.5f), direction, out rayHit, range, whatIsEnemy))        {
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                //damage enemy here
                Debug.DrawLine(fpsCam.transform.position, rayHit.point, Color.red, 20.0f, false);
                Debug.Log(fpsCam.transform.position - rayHit.point);
                rayHit.collider.GetComponent<Player>().TakeDamage(damage, currentPlayer.playerName);
                
            }
            if (rayHit.collider.CompareTag("Wall"))
            {
                Debug.DrawLine(fpsCam.transform.position, rayHit.point, Color.green, 20.0f, false);
                Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }

        }

        //shake camera

        //graphics        
        muzzleFlash.Play();

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

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
        int ammoCount = 0;
        int missingBullet = (magazineSize - bulletsLeft) / bulletsPerTap;
        foreach(Item item in inventory.items)
        {
            if(item.GetType().Name == "Ammo")
            {
                if(item.getAmmoType() == ammoType)
                {
                    ammoCount += item.amount;
                }
            }
        }
        int bulletreload = Mathf.Min(ammoCount * bulletsPerTap, magazineSize);
        bulletsLeft = bulletreload;
        foreach(Item item in inventory.items)
        {
            if(item.GetType().Name == "Ammo")
            {
                if(item.getAmmoType() == ammoType)
                {
                    int bulletremove = Mathf.Min(missingBullet, item.amount);
                    missingBullet -= bulletremove;
                    item.amount -= bulletremove;
                }
            }
        }
        inventory.removeAllZeroItem();
        reloading = false;
    }
}
