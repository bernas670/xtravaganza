using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerelacKiller : FireWeapon {
    public void Awake(){
        _damage = 10;
        _range = 15f;

        _fireRate = 15f;
        _timeToFire= 1f;
    }
}
