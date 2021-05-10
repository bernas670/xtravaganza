using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : Action
{
    public override void Act(EnemyController controller){
        controller.getAgent().destination = controller.getWayPointList() [controller.nextWayPoint].position;
        //controller.getAgent().isStopped = false;

        if (controller.getAgent().remainingDistance <= controller.getAgent().stoppingDistance && !controller.getAgent().pathPending) 
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.getWayPointList().Count;
        }
    }
}