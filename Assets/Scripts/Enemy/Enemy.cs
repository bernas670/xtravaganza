using UnityEngine;

public class Enemy : Character
{
    void Awake()
    {
        _healthStat = new HealthStat(50);
    }

    public override void Die()
    {
        EnemyController controller = gameObject.GetComponent<EnemyController>();
        controller.dropWeapon();

        Destroy(gameObject, 0);
    }


}