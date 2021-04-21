using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MeleeWeapon {
    public void Awake(){
        _damage = 15;
        _range = 5f;

        _fireRate = 8f;
        _timeToFire= 1f;
    }
}
