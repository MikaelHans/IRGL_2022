using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Player : MonoBehaviourPun
{
    public float maxHealth = 100f;
    public float currentHealth = 100.0f;
    public float points = 0;
    public Image healthBar;
    public string playerName = "";
    Camera playerCam;
    Canvas playerCanvas;
    public Camera MinimapCamera;
    public InventoryUI inventoryUI;
    public Inventory inventory;
    public ChracterPickUpWeapon weapons;
    public Animator animator;
    int team_id;
    [SerializeField]
    ItemData helmet, armor;

    public GameObject[] armor_model = new GameObject[3];
    public GameObject[] helmet_model = new GameObject[3];
    public GameObject[] bag_model = new GameObject[3];

    public GameObject chest;

    public int Team_id { get => team_id; set => team_id = value; }
    public ItemData Helmet { get => helmet; set => helmet = value; }
    public ItemData Armor { get => armor; set => armor = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }
    void Start()
    {
        playerCam = gameObject.GetComponentInChildren<Camera>();
        playerCanvas = gameObject.GetComponentInChildren<Canvas>();
        //inventoryUI = GetComponentInChildren<InventoryUI>();
        if (!photonView.IsMine)
        {
            //playerCam.enabled = false;
            //playerCam.gameObject.SetActive(false);
            gameObject.GetComponent<PlayerMovement>().fpsCam.gameObject.SetActive(false);
            MinimapCamera.enabled = false;
            playerCanvas.enabled = false;
        }
        gameObject.name = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        //check player health
        if (currentHealth <= 0)
        {
            Death();
        }
    }



    public float TakeDamage(float damage, string damagerName)
    {
        photonView.RPC("rpc_TakeDamage", RpcTarget.All, damage, damagerName);
        return currentHealth - damage;
    }

    [PunRPC]
    public void rpc_TakeDamage(float damage, string damagerName)
    {
        //if (damagerName != playerName)
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
    }

    public void RecoverHealth(float healthRestored)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthRestored);
    }


    public void Death()
    {
        //Death function        
        if (photonView.IsMine)
        {
            // inventoryUI.removeAll();
            //weapons.dropgunFromSlot();
            List<ItemData> allitems = new List<ItemData>();
            // foreach (GameObject weapon in weapons.dropgunAllGun())
            // {
            //     allitems.Add(weapon);
            // }
            foreach (ItemData item in inventoryUI.inventory.getAllItem())
            {
                allitems.Add(item);
            }
            //export weapon data to itemdata
            WeaponData[] allweapons = weapons.weapon.ToArray();//get weapons array
            foreach(WeaponData weapon in allweapons)
            {
                allitems.Add(weapon);
            }

            allitems.Add(Helmet);
            allitems.Add(Armor);  
            //export allitem to json
            string json = JsonHelper.ToJson<ItemData>(allitems.ToArray());
            //rpc call
            photonView.RPC("sync_item_in_chest", RpcTarget.All, json);
            Debug.Log(json);

        }
        else
        {
            //Destroy(gameObject);
        }
        //chest.GetComponent<UnlockableChest>().fillChest();


    }

    [PunRPC]
    public void sync_item_in_chest(string json)
    {
        if (photonView.IsMine)
        {
            GameObject chest = PhotonNetwork.Instantiate("Prefabs/Chest",transform.position, transform.rotation, 0);
            chest.GetComponent<UnlockableChest>().sync_chest(json);
            PhotonNetwork.Destroy(gameObject);
        }     
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnPhotonPlayerDisconnected");
        SceneManager.LoadScene("GameOver");
    }
}
