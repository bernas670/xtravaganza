public class M4A1 : FireWeapon {
    public void Awake(){
        _damage = 15;
        _range = 30f;

        _fireRate = 5f;
        _timeToFire= 1f;        
        _initialAmmo = 30;

        setAmmoValue(90);
        setClipValue(_initialAmmo);
    }
}
