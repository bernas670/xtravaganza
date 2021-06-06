using UnityEngine;
using TMPro;
public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;

    public WeaponUI weaponUI;

    public float pickUpRange = 5;

    void Awake()
    {
        setPoV(cam.transform);
    }

    void UpdateText()
    {
        if (fireWeapon){
            //weaponUI.SetAmmo(fireWeapon.getClipValue(), fireWeapon.getReloadValue());
            //weaponUI.SetWeaponName(fireWeapon.gameObject.name);
        }
        else{
            //weaponUI.SetAmmo(0, 0);
            //weaponUI.SetWeaponName("None");
        }
    }

    void Update()
    {
        UpdateText();

        if (!fireWeapon) return;

        if (Input.GetButton("Fire1") && fireWeapon.getClipValue() > 0)
        {
            fireWeapon.shoot(this);
        }
        else if (Input.GetButton("Fire2"))
        {
            //meleeWeapon.shoot(this);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            fireWeapon.reload();
        }
    }

}