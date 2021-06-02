using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponSwitchController : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Player player;
    private PlayerShootController _shooter;
    private Transform _cameraTransform;
    public List<GameObject> weapons = new List<GameObject>();
    private int maxWeapons = 4;
    public GameObject currentWeapon;
    public static bool slotFull;

    private Transform _player_ref_RH;
    private Transform _player_ref_LH;

    void Start()
    {
        _shooter = player.gameObject.GetComponent<PlayerShootController>();
        
        weapons.Add(_shooter.getFireWeapon().gameObject);
        currentWeapon = _shooter.getFireWeapon().gameObject;

        _cameraTransform = _shooter.getPoV();
        
        _player_ref_RH = GameObject.Find("Alien").transform.Find("player_ref_right_hand");
        _player_ref_LH = GameObject.Find("Alien").transform.Find("player_ref_left_hand");

      
        SelectWeapon();
    }

    void Update()
    {
        int previousWeapon = selectedWeapon;
        GameObject rightHand = GameObject.Find("RightHandIK");
        GameObject leftHand = GameObject.Find("LeftHandIK");
        
        if(!currentWeapon){
            //Update weapon references. 
            if(rightHand && leftHand){
                rightHand.GetComponent<TwoBoneIKConstraint>().weight = 0;
                leftHand.GetComponent<TwoBoneIKConstraint>().weight = 0;
                rightHand.GetComponent<TwoBoneIKConstraint>().data.target = null;
                leftHand.GetComponent<TwoBoneIKConstraint>().data.target = null;
            }   
        }
        else {
            //Update weapon references. 
            if(rightHand && leftHand){
                Debug.Log(currentWeapon);
                Debug.Log(currentWeapon.gameObject.transform.Find("ref_right_hand"));
                rightHand.GetComponent<TwoBoneIKConstraint>().data.target = currentWeapon.gameObject.transform.Find("ref_right_hand");
                leftHand.GetComponent<TwoBoneIKConstraint>().data.target = currentWeapon.gameObject.transform.Find("ref_left_hand");
                rightHand.GetComponent<TwoBoneIKConstraint>().weight = 1;
                leftHand.GetComponent<TwoBoneIKConstraint>().weight = 1;
            }   
        }

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
        else if (Input.GetKeyDown(KeyCode.Q) && currentWeapon) {
        
            DropWeapon();
        }

        // If selected weapon changed, call selectweapon method to perform the weapon change
        if (previousWeapon != selectedWeapon)
            SelectWeapon();

        else if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.CompareTag("FireWeapon")) {
                    FireWeapon weapon = hit.transform.gameObject.GetComponent<FireWeapon>();
                    if (Input.GetKeyDown(KeyCode.E) && weapons.Count < maxWeapons && !weapon.isInUse())
                    {            
                        PickWeapon(_cameraTransform.GetChild(0), hit.collider);
                    }
                }
            }
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (GameObject weapon in weapons)
        {
            // if the weapon is selected set true, if it's not set false
            weapon.SetActive(i == selectedWeapon);
            if (i == selectedWeapon)
            {
                FireWeapon fWeapon = weapon.GetComponent<FireWeapon>();
                _shooter.setFireWeapon(fWeapon);
                currentWeapon = weapon;
            }
            i++;
        }
    }

    void PickWeapon(Transform container, Collider coll)
    {   
    
        if(weapons.Count == 0 ){
            _shooter.setFireWeapon(coll.gameObject.GetComponent<FireWeapon>());
            currentWeapon = coll.gameObject;
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

        //Make Rigidbody kinematic and BoxCollider a trigger
        coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        coll.gameObject.GetComponent<FireWeapon>().enabled = true;
    }

    void DropWeapon(){

        currentWeapon.GetComponent<FireWeapon>().setInUse(false);

        slotFull = false;

        // First ensure we remove our hand as parent for the weapon
        currentWeapon.transform.parent = null;

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        BoxCollider coll = currentWeapon.GetComponent<BoxCollider>();
 
        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.gameObject.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(_cameraTransform.forward * 2, ForceMode.Impulse);
        rb.AddForce(_cameraTransform.up * 2, ForceMode.Impulse);
        
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        currentWeapon.GetComponent<FireWeapon>().enabled = false;

        _shooter.setFireWeapon(null);

        weapons.Remove(currentWeapon);

        if(weapons.Count > 0){
            _shooter.setFireWeapon(weapons[0].GetComponent<FireWeapon>());
            currentWeapon = weapons[0];
            currentWeapon.SetActive(true);
        }else currentWeapon = null;
    }
}