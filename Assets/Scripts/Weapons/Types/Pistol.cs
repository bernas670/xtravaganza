public class Pistol : FireWeapon
{
    public void Awake()
    {
        _damage = 20;
        _range = 40f;

        _fireRate = 2.5f;
        _initialAmmo = 15;

        setReloadValue(30);
        setClipValue(_initialAmmo);
    }

    public override string getType()
    {
        return "Pistol";
    }
}
