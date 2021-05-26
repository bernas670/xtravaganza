using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : FireWeapon {
    public void Awake(){
        _damage = 20;
        _range = 30f;

        _fireRate = 2.5f;
        _timeToFire= 1f;        
        _initialAmmo = 15;

        setAmmoValue(30);
        setClipValue(_initialAmmo);
    }
}
