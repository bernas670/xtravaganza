public class HealthStat
{
    protected int _health;

    public HealthStat(int init)
    {
        _health = init;
    }

    public void setHealth(int health)
    {
        _health = health;
    }
    
    public int getHealth()
    {
        return _health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public void TakePowerUp(int powerup)
    {
        _health += powerup;
    }

    public bool isDead()
    {
        return _health <= 0;
    }
}
