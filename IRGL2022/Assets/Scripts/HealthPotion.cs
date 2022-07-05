

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
}
