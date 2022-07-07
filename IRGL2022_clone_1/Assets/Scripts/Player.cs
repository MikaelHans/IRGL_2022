using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
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
    public Canvas playerCanvas;
    public Camera MinimapCamera;
    public InventoryUI inventoryUI;
    public Inventory inventory;
    public ChracterPickUpWeapon weapons;
    public Animator animator;
    int team_id;
    [SerializeField]
    ItemData helmet, armor, bag;

    public GameObject[] armor_model = new GameObject[3];
    public GameObject[] helmet_model = new GameObject[3];
    public GameObject[] bag_model = new GameObject[3];

    public GameObject chest;
    public bool is_same_team;
    public TextMeshProUGUI playername_ui;

    public int Team_id { get => team_id; set => team_id = value; }
    public ItemData Helmet { get => helmet; set => helmet = value; }
    public ItemData Armor { get => armor; set => armor = value; }
    public ItemData Bag { get => bag; set => bag = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        inventory = gameObject.GetComponent<Inventory>();

    }
    void Start()
    {
        //inventoryUI = GetComponentInChildren<InventoryUI>();
        playerName = photonView.Owner.NickName;
        if (photonView.IsMine)//if is this client player
        {
            playerCam = gameObject.GetComponentInChildren<Camera>();
            playerCanvas = gameObject.GetComponentInChildren<Canvas>();
        }
        else
        {
            //playerCam.enabled = false;
            //playerCam.gameObject.SetActive(false);
            gameObject.GetComponent<PlayerMovement>().fpsCam.gameObject.SetActive(false);
            MinimapCamera.enabled = false;
            playerCanvas.enabled = false;

            List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
            Player myPlayer = allPlayers.Find(player => player.photonView.IsMine);
            if(myPlayer != null)
            {
                if (myPlayer.photonView.Owner.NickName == playerName)
                {
                    //same team codes
                    is_same_team = true;
                    playername_ui.gameObject.SetActive(true);
                    playername_ui.text = playerName;
                    playername_ui.gameObject.GetComponent<UI_Follow>().maincamera = myPlayer.playerCam;
                }
                else
                {
                    is_same_team = false;
                }
            }                       
        }
        
        gameObject.name = photonView.Owner.NickName;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        //check player health
        if (currentHealth <= 0 && photonView.IsMine)
        {
            Death();
        }
    }

    public float TakeDamage(float damage, int teamID)
    {
        photonView.RPC("rpc_TakeDamage", RpcTarget.All, damage, teamID);
        return currentHealth - damage;
    }

    [PunRPC]
    public void rpc_TakeDamage(float damage, int teamID)
    {
        //if (damagerName != playerName)
        if(photonView.IsMine)
        {
            float defense = Armor.prefab.GetComponent<Equipable>().defense;
            currentHealth -= damage - damage * (defense / 100);
            if (currentHealth <= 0)
            {
                //Player [] players = FindObjectsOfType<Player>();
                #region Update Team Score Data

                #endregion
                Death();
            }
        }            
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
            foreach (ItemData item in inventory.getAllItem())
            {
                allitems.Add(item);
            }
            //export weapon data to itemdata
            WeaponData[] allweapons = weapons.weapon.ToArray();//get weapons array
            foreach (WeaponData weapon in allweapons)
            {
                if(weapon._gunsystem != null)
                {
                    allitems.Add(weapon);
                }                
            }

            if(Helmet.prefab != null)
            {
                allitems.Add(Helmet);
            }
            if (Armor.prefab != null)
            {
                allitems.Add(Armor);
            }
            if (Bag.prefab != null)
            {
                allitems.Add(Bag);
            }
            
            
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
            GameObject chest = PhotonNetwork.Instantiate("Prefabs/Chest", transform.position, transform.rotation, 0);
            chest.GetComponent<UnlockableChest>().sync_chest(json);
            PhotonNetwork.Destroy(gameObject);
        }  
        //if (photonView.IsMine)
        //{
        //    GameObject chest = PhotonNetwork.Instantiate("Prefabs/Chest",transform.position, transform.rotation, 0);
        //    chest.GetComponent<UnlockableChest>().sync_chest(json);
        //    PhotonNetwork.Destroy(gameObject);
        //}     
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnPhotonPlayerDisconnected");
        SceneManager.LoadScene("GameOver");
    }
}
