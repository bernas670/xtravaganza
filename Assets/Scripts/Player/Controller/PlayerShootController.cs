using UnityEngine;
using TMPro;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;
    public TextMeshProUGUI bulletsText;

    void Awake()
    {
        setPoV(cam.transform);
        UpdateText();
    }

    void UpdateText()
    {
        bulletsText.text = string.Format("ammo: {0}", fireWeapon.getClipValue());
    }

    void Update()
    {
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
    }

}