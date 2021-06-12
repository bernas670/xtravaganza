using UnityEngine;

public class Character : MonoBehaviour
{
    protected HealthStat _healthStat;
    protected bool _isDead;

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

    public void setIsDead(){
        _isDead = true;
    }

    public bool isDead(){
        return _isDead;
    }



}