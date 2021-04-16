using UnityEngine;

/* Player and enemies should extend this */
public abstract class Shooter : MonoBehaviour
{

    [SerializeField] protected Weapon weapon;

    public abstract void shoot();
    public void setWeapon(Weapon weapon){
        this.weapon = weapon;
    }
}