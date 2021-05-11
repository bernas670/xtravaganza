using UnityEngine;


public class Enemy : Character
{

    void Awake()
    {
        _health = new HealthStat(50);
    }

    public override void Die(){
        EnemyController controller = gameObject.GetComponent<EnemyController>();
        PickDropController pickdrop = controller.getFireWeapon().GetComponent<PickDropController>();
        pickdrop.Drop(true);
        
        Destroy(gameObject, 0);
    }

}