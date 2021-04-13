using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{
    [SerializeField] int MAX_HEALTH = 100; /*health compensa ser stat se houver powerups */
    public override void Start(){
        _health = MAX_HEALTH;
    }

}
