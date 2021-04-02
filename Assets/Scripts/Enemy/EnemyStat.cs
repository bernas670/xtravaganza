using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    [SerializeField] int MAX_HEALTH = 100; /*health compensa ser stat se houver powerups */

    public override void Start(){
        _health = MAX_HEALTH;
    }

    /* for testing */
    /*void Update(){
        if(Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(10);
            Debug.Log(_health);
        }
    }*/
}
