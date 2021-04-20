using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerelacKiller : FireWeapon {
    public void Awake(){
        _damage = 10;
        _range = 15f;

        _fireRate = 5f;
        _timeToFire= 1f;
        setAmmoValue(90);
        setClipValue(30);
        _initialAmmo = 30;
    }

 
}
