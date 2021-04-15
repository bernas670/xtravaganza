using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    protected int _damage;
    protected float _range;
    protected AmmoStat ammo; /* nr de bullets + modifiers para saber qts reloads ? */

    public int getDamage()
    {
        return _damage;
    }

    public float getRange(){
        return _range;
    }

    public void reload(){
        // TODO
    }
}
