using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingAction : Action
{

    public override void Act(EnemyController controller)
    {
        controller.getAgent().isStopped = true;
    }
}
