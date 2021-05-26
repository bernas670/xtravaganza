using UnityEngine;

/* Player and enemies should extend this */
public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected FireWeapon fireWeapon;
    [SerializeField] protected MeleeWeapon meleeWeapon;

    private Transform point_of_view;

    public MeleeWeapon getMeleeWeapon()
    {
        return this.meleeWeapon;
    }

    public void setPoV(Transform pov)
    {
        point_of_view = pov;
    }

    public Transform getPoV()
    {
        return point_of_view;
    }

    public void setFireWeapon(FireWeapon weapon)
    {
        this.fireWeapon = weapon;
    }
    public FireWeapon getFireWeapon()
    {
        return this.fireWeapon;
    }

    public void setMeleeWeapon(MeleeWeapon weapon)
    {
        this.meleeWeapon = weapon;
    }
}