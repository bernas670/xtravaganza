using UnityEngine;
using TMPro;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;
    private PickDropController pickDrop;

    public WeaponUI weaponUI;

    public float pickUpRange = 5;

    void Awake()
    {
        setPoV(cam.transform);
        pickDrop = gameObject.GetComponentInChildren<PickDropController>();
    }

    void UpdateText()
    {
        if (fireWeapon)
            weaponUI.SetAmmo(fireWeapon.getClipValue(), fireWeapon.getReloadValue());
        // bulletsText.text = string.Format("ammo: {0}", fireWeapon.getClipValue());
        else
            weaponUI.SetAmmo(0, 0);
    }

    void Update()
    {
        UpdateText();


        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (!Physics.Raycast(base.getPoV().position, base.getPoV().forward, out hit, pickUpRange)) return;

            PickDropController newPickDrop = hit.transform.gameObject.GetComponent<PickDropController>();
            if (newPickDrop)
            {
                if (fireWeapon)
                {
                    this.drop();
                }
                this.pick(newPickDrop);
            }
        }


        if (!fireWeapon)
        {
            return;
        }

        if (Input.GetButton("Fire1") && fireWeapon.getClipValue() > 0)
        {
            fireWeapon.shoot(this);
        }
        else if (Input.GetButton("Fire2"))
        {
            meleeWeapon.shoot(this);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            fireWeapon.reload();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            this.drop();
        }
    }

    public void drop()
    {
        fireWeapon = null;
        pickDrop.Drop(gameObject.GetComponent<Rigidbody>().velocity, cam.transform);
        pickDrop = null;
    }

    public void pick(PickDropController newPickDrop)
    {
        pickDrop = newPickDrop;
        fireWeapon = pickDrop.fireWeapon;
        pickDrop.PickUp(cam.transform.GetChild(0));
    }

}