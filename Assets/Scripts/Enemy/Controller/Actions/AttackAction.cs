using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AttackAction : Action{
    public override void Act(EnemyController controller){}
}
public class MeleeAction : AttackAction {
 public override void Act(EnemyController controller){

    if (Time.time >= controller.getMeleeWeapon().getTimeToFire())
        {
            controller.getMeleeWeapon().setTimeToFire(Time.time + 1f / controller.getMeleeWeapon().getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit, controller.getMeleeWeapon().getRange()))
            {   
                if (hit.transform.name == "Player")
                {
                    Debug.Log("MELEE : Enemy attacked");
                    Player _playerStat = controller._player.GetComponent<Player>();
                    _playerStat.TakeDamage(controller.getMeleeWeapon().getDamage());
                }
            }
        }
    }
}
public class ShootAction : AttackAction {
      public override void Act(EnemyController controller){
         if (Time.time >= controller.getFireWeapon().getTimeToFire())
        {
            controller.getFireWeapon().setTimeToFire(Time.time + 1f / controller.getFireWeapon().getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit, controller.getFireWeapon().getRange()))
            {   
                if (hit.transform.name == "Player")
                {
                    Debug.Log("FireWeapon : Enemy attacked");
                    Player _playerStat = controller._player.GetComponent<Player>();
                    _playerStat.TakeDamage(controller.getFireWeapon().getDamage());
                }
            }
        }
    }
    
}