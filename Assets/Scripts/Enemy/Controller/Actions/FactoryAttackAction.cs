using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FactoryAttackAction
{
    public AttackAction createAttackAction(EnemyController controller, float distance){

        AttackAction action;
        
        if (distance <= controller.getMeleeWeapon().getRange()){
            action =  new MeleeAction();
        } else {
            action = new ShootAction();
        }

        return action;

    }
}