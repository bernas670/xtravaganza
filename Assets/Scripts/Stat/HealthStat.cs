

using UnityEngine;

public class HealthStat
{
    protected int _health;
    
    public HealthStat(int init){
        _health=init;
    }
    public void setHealth(int health){
        _health=health;
    }

    public void TakeDamage(int damage) {
        _health -= damage;
        if(_health < 0)
            Die();
    }

    public void TakePowerUp(int powerup){
        _health+=powerup;
    }

    public virtual void Die(){
        Debug.Log("Dieded");
    }
}
