using UnityEngine;

public class Character : MonoBehaviour
{
    protected HealthStat _healthStat;

    public HealthStat getHealth()
    {
        return _healthStat;
    }

    public virtual void TakeDamage(int damage)
    {
        if (_healthStat.isDead())
        {
            return;
        }

        _healthStat.TakeDamage(damage);

        if (_healthStat.isDead())
        {
            this.Die();
        }
    }

    public virtual void Die() { }

}