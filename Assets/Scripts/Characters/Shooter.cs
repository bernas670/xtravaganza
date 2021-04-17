using UnityEngine;

/* Player and enemies should extend this */
public abstract class Shooter : MonoBehaviour
{

    protected Weapon weapon;
    [SerializeField] protected FireWeapon cerelac;
    [SerializeField] protected MeleeWeapon dagger;

    public void setWeapon(Weapon weapon){
        this.weapon = weapon;
    }

    public Weapon getWeapon(){
        return this.weapon;
    }
}