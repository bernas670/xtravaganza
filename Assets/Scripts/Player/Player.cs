using UnityEngine;

public class Player : Character
{
    void Awake()
    {
        _health = new HealthStat(100);
    }
}