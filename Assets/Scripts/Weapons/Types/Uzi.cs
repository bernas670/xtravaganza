public class Uzi : FireWeapon
{
    public void Awake()
    {
        _damage = 7;
        _range = 50f;

        _fireRate = 7.5f;
        _initialAmmo = 45;

        setAmmoValue(135);
        setClipValue(_initialAmmo);
    }

    public override string getType()
    {
        return "Rifle";
    }
}
