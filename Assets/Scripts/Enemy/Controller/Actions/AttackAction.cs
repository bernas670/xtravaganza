using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AttackAction : Action
{
    public override void Act(EnemyController controller) { }
}

public class MeleeAction : AttackAction
{
    public override void Act(EnemyController controller)
    {
        controller.getMeleeWeapon().shoot(controller);
    }
}
public class ShootAction : AttackAction
{
    public override void Act(EnemyController controller)
    {
        controller.getFireWeapon().shoot(controller);
    }
}