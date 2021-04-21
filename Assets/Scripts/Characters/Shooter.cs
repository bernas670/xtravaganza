using UnityEngine;

/* Player and enemies should extend this */
public abstract class Shooter : MonoBehaviour
{

    [SerializeField] protected FireWeapon fireWeapon;
    [SerializeField] protected MeleeWeapon meleeWeapon;

    public void setFireWeapon(FireWeapon weapon){
        this.fireWeapon = weapon;
    }
    public FireWeapon getFireWeapon(){
        return this.fireWeapon;
    }
      public void setMeleeWeapon(MeleeWeapon weapon){
        this.meleeWeapon = weapon;
    }

    public MeleeWeapon getMeleeWeapon(){
        return this.meleeWeapon;
    }
}