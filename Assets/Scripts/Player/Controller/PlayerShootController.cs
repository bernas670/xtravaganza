using UnityEngine;
using TMPro;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;
    public TextMeshProUGUI bulletsText;
    private PickDropController pickDrop;

    public float pickUpRange = 5;

    void Awake()
    {
        setPoV(cam.transform);
        pickDrop = gameObject.GetComponentInChildren<PickDropController>();
    }

    void UpdateText()
    {
        bulletsText.text = string.Format("ammo: {0}", fireWeapon.getClipValue());
    }

    void Update()
    {
        UpdateText();
        if (!fireWeapon)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (!Physics.Raycast(base.getPoV().position, base.getPoV().forward, out hit, pickUpRange)) return;

                PickDropController newPickDrop = hit.transform.gameObject.GetComponent<PickDropController>();
                if (newPickDrop)
                {
                    this.pick(newPickDrop);
                }
            }

            return;
        }

        if (Input.GetButton("Fire1") && fireWeapon.getClipValue() > 0)
        {
            fireWeapon.shoot(this);
            UpdateText();
        }
        else if (Input.GetButton("Fire2"))
        {
            meleeWeapon.shoot(this);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            fireWeapon.reload();
            UpdateText();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            this.drop();
        }
    }

    public void drop()
    {
        fireWeapon = null;
        pickDrop.Drop(gameObject.GetComponent<Rigidbody>().velocity);
        pickDrop = null;
    }

    public void pick(PickDropController newPickDrop)
    {
        pickDrop = newPickDrop;
        fireWeapon = pickDrop.fireWeapon;
        pickDrop.PickUp(cam.transform.GetChild(0));
    }

}