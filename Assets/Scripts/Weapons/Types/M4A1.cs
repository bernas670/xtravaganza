public class M4A1 : FireWeapon
{
    public void Awake()
    {
        _damage = 30;
        _range = 60f;

        _fireRate = 3f;
        _initialAmmo = 20;

        setReloadValue(60);
        setClipValue(_initialAmmo);
    }

    public override string getType()
    {
        return "Rifle";
    }
}
