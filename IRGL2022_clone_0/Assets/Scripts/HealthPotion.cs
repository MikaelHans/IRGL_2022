

public class HealthPotion : Item, IStoreable, IUsable
{
    float healthRestored = 25.0f;

    public void Use(Player user)
    {
        if (user.currentHealth < user.maxHealth)
        {
            user.RecoverHealth(healthRestored);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
