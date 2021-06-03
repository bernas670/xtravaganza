public class Uzi : FireWeapon {
    public void Awake(){
        _damage = 10;
        _range = 15f;

        _fireRate = 7.5f;
        _timeToFire= 1f;        
        _initialAmmo = 45;

        setAmmoValue(135);
        setClipValue(_initialAmmo);
    }
}
