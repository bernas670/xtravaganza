using UnityEngine;


public class Enemy : Character
{

    void Awake()
    {
        _health = new HealthStat(50);
    }


}