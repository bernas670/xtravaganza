using UnityEngine;

public class Character : MonoBehaviour
{
    protected HealthStat _health;


    public HealthStat getHealth()
    {
        return _health;
    }

    public void TakeDamage(int damage){
        _health.TakeDamage(damage);
    }

}