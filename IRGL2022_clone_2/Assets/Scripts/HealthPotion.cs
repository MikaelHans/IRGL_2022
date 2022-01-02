

public class HealthPotion : Item
{
    float healthRestored = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use(Player user)
    {
        base.Use(user);
        if(user.currentHealth < user.maxHealth)
        {
            user.RecoverHealth(healthRestored);
            amount -= 1;
        }        
    }
}
