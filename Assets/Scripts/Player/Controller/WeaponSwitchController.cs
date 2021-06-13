using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{
    public static bool slotFull;
    
    public WeaponUI weaponUI;
    public int selectedWeapon = 0;
    public Player player;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject currentWeapon;
    [FMODUnity.EventRef]
    public string pickEvent;
    public float pickSoundInterval = 0.5f;

    private Animator _animator;
    private PlayerShootController _shooter;
    private RigController _rig;
    private Transform _cameraTransform;
    private int maxWeapons = 4;
    private float lastPickSoundTime = 0;

    void Start()
    {
        _shooter = player.gameObject.GetComponent<PlayerShootController>();
        _rig = player.gameObject.GetComponentsInChildren<RigController>()[0];
        _animator = player.gameObject.GetComponentsInChildren<Animator>()[0];

        weapons.Add(_shooter.getFireWeapon().gameObject);
        currentWeapon = _shooter.getFireWeapon().gameObject;
        currentWeapon.GetComponent<BoxCollider>().enabled = false;

        _cameraTransform = _shooter.getPoV();

        SelectWeapon();
        weaponUI.updateWeaponsList(weapons);
    }

    void Update()
    {
        int previousWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && weapons.Count > 1)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && weapons.Count > 1)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentWeapon)
        {
            DropWeapon();
        }

        // If selected weapon changed, call selectweapon method to perform the weapon change
        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
            if (Time.time - lastPickSoundTime >= pickSoundInterval)
            {
                PlayPickSound();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("FireWeapon"))
                {
                    FireWeapon weapon = hit.transform.gameObject.GetComponent<FireWeapon>();
                    if (Input.GetKeyDown(KeyCode.E) && weapons.Count < maxWeapons && !weapon.isInUse())
                    {
                        PickWeapon(_cameraTransform.GetChild(0), hit.collider);
                    }
                }
            }
        }
    }

    void PlayPickSound()
    {
        lastPickSoundTime = Time.time;
        FMODUnity.RuntimeManager.PlayOneShotAttached(pickEvent, _cameraTransform.gameObject);
    }

    void SelectWeapon()
    {
        int i = 0;
        _animator.SetBool("has" + currentWeapon.GetComponent<FireWeapon>().getType(), false);
        foreach (GameObject weapon in weapons)
        {
            // if the weapon is selected set true, if it's not set false
            weapon.SetActive(i == selectedWeapon);
            if (i == selectedWeapon)
            {
                FireWeapon fWeapon = weapon.GetComponent<FireWeapon>();
                _shooter.setFireWeapon(fWeapon);
                currentWeapon = weapon;
                _animator.SetBool("has" + fWeapon.getType(), true);

                _rig.updateRigWeaponReference(currentWeapon.transform.Find("ref_right_hand"), currentWeapon.transform.Find("ref_left_hand"));
            }
            i++;
        }
    }

    void PickWeapon(Transform container, Collider coll)
    {
        if (weapons.Count == 0)
        {
            FireWeapon fireWeapon = coll.gameObject.GetComponent<FireWeapon>();
            _shooter.setFireWeapon(fireWeapon);
            _animator.SetBool("has" + fireWeapon.getType(), true);
            currentWeapon = coll.gameObject;
            _rig.updateRigWeaponReference(currentWeapon.transform.Find("ref_right_hand"), currentWeapon.transform.Find("ref_left_hand"));
        }

        // hides the weapon because it's now in our 'inventory'
        coll.gameObject.SetActive(weapons.Count == 0);

        // save the weapon                
        weapons.Add(coll.gameObject);

        coll.gameObject.GetComponent<FireWeapon>().setInUse(true);

        if (weapons.Count == maxWeapons) slotFull = true;

        //Make weapon a child of the camera and move it to default position
        coll.transform.SetParent(container);
        coll.transform.localPosition = Vector3.zero;
        coll.transform.localRotation = Quaternion.Euler(0, 0, 0);

        //Make Rigidbody kinematic, enable BoxCollider and make it a trigger
        coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        coll.enabled = false;
        coll.isTrigger = true;

        //Enable script
        coll.gameObject.GetComponent<FireWeapon>().enabled = true;
        PlayPickSound();
        weaponUI.updateWeaponsList(weapons);

    }

    void DropWeapon()
    {
        FireWeapon fireWeapon = currentWeapon.GetComponent<FireWeapon>();
        fireWeapon.setInUse(false);

        slotFull = false;

        // First ensure we remove our hand as parent for the weapon
        currentWeapon.transform.parent = null;

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        BoxCollider coll = currentWeapon.GetComponent<BoxCollider>();

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;
        coll.enabled = true;

        //Gun carries momentum of player
        rb.velocity = player.gameObject.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(_cameraTransform.forward * 2, ForceMode.Impulse);
        rb.AddForce(_cameraTransform.up * 2, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        fireWeapon.enabled = false;
        _animator.SetBool("has" + fireWeapon.getType(), false);

        _shooter.setFireWeapon(null);

        weapons.Remove(currentWeapon);

        if (weapons.Count > 0)
        {
            FireWeapon newFireWeapon = weapons[0].GetComponent<FireWeapon>();
            _shooter.setFireWeapon(newFireWeapon);
            _animator.SetBool("has" + newFireWeapon.getType(), true);
            currentWeapon = weapons[0];
            currentWeapon.SetActive(true);
            _rig.updateRigWeaponReference(currentWeapon.transform.Find("ref_right_hand"), currentWeapon.transform.Find("ref_left_hand"));
        }
        else
        {
            currentWeapon = null;
            _rig.clearRigWeaponReference();
        }

        weaponUI.updateWeaponsList(weapons);
    }
}