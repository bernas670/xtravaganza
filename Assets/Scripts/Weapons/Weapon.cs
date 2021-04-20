using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    protected int _damage;
    protected float _range;

    [SerializeField] protected float _fireRate;
    protected float _timeToFire;

    public int getDamage()
    {
        return _damage;
    }

    public float getRange(){
        return _range;
    }

    public float getFireRate(){
        return _fireRate;
    }

    public float getTimeToFire(){
        return _timeToFire;
    }

    public void setTimeToFire(float time){
        _timeToFire = time;
    }

    public abstract void shoot(Shooter controller);
}
