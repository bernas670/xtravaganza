using UnityEngine;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;

    public WeaponUI weaponUI;
    public Animator animator;
    public float pickUpRange = 5;

    private MovementController _movementController;
    private bool _isMeleeing = false;

    void Awake()
    {
        setPoV(cam.transform);
        _movementController = GetComponent<MovementController>();
    }

    void UpdateText()
    {
        if (fireWeapon)
        {
            weaponUI.SetAmmo(fireWeapon.getClipValue(), fireWeapon.getReloadValue());
            weaponUI.SetWeaponName(fireWeapon.gameObject.name);
        }
        else
        {
            weaponUI.SetAmmo(0, 0);
            weaponUI.SetWeaponName("None");
        }
    }

    void Update()
    {
        if (PauseController.isPaused)
        {
            return;
        }

        UpdateText();
        _isMeleeing = animator.GetBool("isMeleeing");

        if (Input.GetButton("Fire2") && !_isMeleeing && !_movementController.IsWallRunning())
        {
            _isMeleeing = true;
            animator.SetBool("isMeleeing", true);
        }

        if (!fireWeapon) return;

        if (Input.GetButton("Fire1") && fireWeapon.getClipValue() > 0)
        {
            fireWeapon.shoot(this);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            fireWeapon.reload();
        }
        
        if (Input.GetButtonUp("Fire1") || fireWeapon.getClipValue() <= 0)
        {
            fireWeapon.StopShootSound();
        }
    }

    public void Kick()
    {
        meleeWeapon.shoot(this);
    }

    public void EndMeleeAnimation()
    {
        _isMeleeing = false;
        animator.SetBool("isMeleeing", false);
    }
}