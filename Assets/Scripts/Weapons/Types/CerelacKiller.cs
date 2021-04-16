using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerelacKiller : Weapon {
    public void Awake(){
        _damage = 10;
        _range = 100f;

        _fireRate = 15f;
        _timeToFire= 1f;
    }
}
