using UnityEngine;

public class Player : Character
{
    void Awake()
    {
        _health = new HealthStat(100);
    }

    public override void Die(){
        Destroy(gameObject, 0);
    }

}