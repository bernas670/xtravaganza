using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    public override void Act(EnemyController controller){

        if (Time.time >= controller.getWeapon().getTimeToFire())
        {
            controller.getWeapon().setTimeToFire(Time.time + 1f / controller.getWeapon().getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, controller.transform.forward, out hit, controller.getWeapon().getRange()))
            {   
                if (hit.transform.name == "Player")
                {
                    Debug.Log("Enemy attacked");
                    Player _playerStat = controller._player.GetComponent<Player>();
                    _playerStat.TakeDamage(controller.getWeapon().getDamage());
                }
            }
        }
    }
}