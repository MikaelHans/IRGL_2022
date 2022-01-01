using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100.0f;
    public Image healthBar;
    public string playerName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage, string damagerName)
    {
        if(damagerName != playerName)
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
