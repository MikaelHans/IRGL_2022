


public class Ammo : Item, IStoreable
{
    public string ammoType = "";
    public override string getAmmoType()
    {
        return ammoType;
    }
}
