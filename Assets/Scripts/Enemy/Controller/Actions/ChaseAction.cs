using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : Action
{
    public override void Act(EnemyController controller){
        faceTarget(controller);
        controller.getAgent().SetDestination(controller.getTarget().position); /*move towards target*/
    }

    private void faceTarget(EnemyController controller)
    {
        Vector3 direction = (controller.getTarget().position - controller.gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.gameObject.transform.rotation = Quaternion.Slerp(controller.gameObject.transform.rotation, lookRotation, Time.deltaTime * 7.5f);
    }  
}