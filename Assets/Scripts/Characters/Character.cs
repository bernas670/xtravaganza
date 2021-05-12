using UnityEngine;

public class Character : MonoBehaviour
{
    protected HealthStat _healthStat;


    public HealthStat getHealth()
    {
        return _healthStat;
    }

    public void TakeDamage(int damage){
        _healthStat.TakeDamage(damage);

        if(_healthStat.isDead()){
            this.Die();
        }
    }

    public virtual void Die(){}

}