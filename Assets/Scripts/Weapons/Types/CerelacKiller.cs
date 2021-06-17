public class CerelacKiller : FireWeapon
{
    public void Awake()
    {
        _damage = 20;
        _range = 60f;

        _fireRate = 3.5f;
        _initialAmmo = 30;

        setReloadValue(90);
        setClipValue(_initialAmmo);
    }

    public override string getType()
    {
        return "Rifle";
    }
}
