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
            List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
            foreach (Player player in allPlayers)
            {
                if (!player.photonView.IsMine)
                {
                    player.playername_ui.GetComponent<UI_Follow>().maincamera = playerCam;
                }
            }
        }
        else
        {
            //playerCam.enabled = false;
            //playerCam.gameObject.SetActive(false);
            gameObject.GetComponent<PlayerMovement>().fpsCam.gameObject.SetActive(false);
            MinimapCamera.enabled = false;
            playerCanvas.enabled = false;
            team_id = (int)photonView.InstantiationData[0];
            playername_ui.gameObject.SetActive(true);
            playername_ui.text = playerName;
            List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
            foreach (Player player in allPlayers)
            {
                if (player.photonView.IsMine)
                {
                    playername_ui.GetComponent<UI_Follow>().maincamera = player.playerCam;
                }
            }
            #region old multiplayer codes
            //List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
            //Player myPlayer = allPlayers.Find(player => player.photonView.IsMine);
            //if(myPlayer != null)
            //{
            //    if (myPlayer.photonView.Owner.NickName == playerName)
            //    {
            //        //same team codes
            //        is_same_team = true;
            //        playername_ui.gameObject.SetActive(true);
            //        playername_ui.text = playerName;
            //        playername_ui.gameObject.GetComponent<UI_Follow>().maincamera = myPlayer.playerCam;
            //    }
            //    else
            //    {
            //        is_same_team = false;
            //    }
            //}
            #endregion
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
            //Death();
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
            float defense = calculateDefense(Armor) + calculateDefense(Helmet);
            float adjustedDamage = damage - damage * defense;
            currentHealth -= adjustedDamage;
            Debug.Log(adjustedDamage);
            if (currentHealth <= 0)
            {
                //Player [] players = FindObjectsOfType<Player>();
                #region Update Team Score Data

                #endregion
                Death(teamID);
            }
        }            
    }

    public float calculateDefense(ItemData DefenseItem)
    {
        if(DefenseItem.prefab == null)
        {
            return 0;
        }
        float defense = DefenseItem.prefab.GetComponent<Equipable>().defense;
        float []multiplier = DefenseItem.prefab.GetComponent<Equipable>().multiplier;
        int level = DefenseItem.level;
        defense *= multiplier[level];

        return defense/100;
    }

    public void RecoverHealth(float healthRestored)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healthRestored);
    }

    public void Death(int killer_team_id)
    {
        //Death function        
        if (photonView.IsMine)
        {
            // inventoryUI.removeAll();
            //weapons.dropgunFromSlot();
            photonView.RPC("update_score", RpcTarget.All, killer_team_id, 100);
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
    public void update_score(int team_id, int score = 100)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            ScoreKeeper scorekeeper = FindObjectOfType<ScoreKeeper>();
            scorekeeper.update_team_score(team_id, score);
        }
    }

    [PunRPC]
    public void sync_item_in_chest(string json)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            
        }
        if (photonView.IsMine)
        {
            GameObject chest = PhotonNetwork.Instantiate("Prefabs/Chest", transform.position, transform.rotation, 0);
            if(chest != null)
            {
                chest.GetComponent<UnlockableChest>().sync_chest(json);
            }
            
            PhotonNetwork.Destroy(gameObject);
            //PhotonNetwork.Disconnect();
            //Application.Quit();
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
