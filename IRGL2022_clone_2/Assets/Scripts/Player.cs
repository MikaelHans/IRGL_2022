using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public float maxHealth = 100f;
    public float currentHealth = 100.0f;
    public Image healthBar;
    public string playerName = "";
    Camera playerCam;
    Canvas playerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = gameObject.GetComponentInChildren<Camera>();
        playerCanvas = gameObject.GetComponentInChildren<Canvas>();
        if (!photonView.IsMine)
        {
            playerCam.enabled = false;
            playerCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage, string damagerName)
    {
        photonView.RPC("rpc_TakeDamage", RpcTarget.All, damage, damagerName);
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
        Destroy(gameObject);
    }
}
