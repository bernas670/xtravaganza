using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{   
    public int selectedWeapon = 0;
    public Player player;
    private PlayerShootController _shooter;

    private Transform _cameraTransform;

     public List<GameObject> weapons;
     public int maxWeapons = 4;
 
     // this variable represent the weapon you carry in your hand 
     public GameObject currentWeapon;
 
     // this variable represent your hand which you set as the parent of your currentWeapon
     public Transform hand;
     public static bool slotFull;

    void Start(){
        _shooter = player.gameObject.GetComponent<PlayerShootController>();
        _cameraTransform = _shooter.getPoV();
        SelectWeapon();
    }

    void Update(){
        int previousWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f){
            Debug.Log("Changed weapon");
            if(selectedWeapon>= transform.childCount - 1)
                selectedWeapon = 0;
            else 
                selectedWeapon++;
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(selectedWeapon<= 0)
                selectedWeapon = transform.childCount - 1;
            else 
                selectedWeapon--;
        }

        // If selected weapon changed, call selectweapon method to perform the weapon change
        if(previousWeapon != selectedWeapon)
            SelectWeapon();

        RaycastHit hit;
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out hit)) {
             if (hit.transform.CompareTag("FireWeapon") && Input.GetKeyDown(KeyCode.E) && weapons.Count < maxWeapons) {
                 
                 PickWeapon(_cameraTransform.GetChild(0),hit.collider);
             }
        }
    }

    void SelectWeapon(){
        int i=0;
        foreach(Transform weapon in transform){
            
            // if the weapon is selected set true, if it's not set false
            weapon.gameObject.SetActive(i == selectedWeapon);
            if(i == selectedWeapon){
                FireWeapon fWeapon = weapon.gameObject.GetComponent<FireWeapon>();
                _shooter.setFireWeapon(fWeapon);
            }
            i++;
        }
    }

    void PickWeapon(Transform container, Collider coll){
             // PICKUP WEAPONS
            // save the weapon                
            weapons.Add(coll.gameObject);
            
            // hides the weapon because it's now in our 'inventory'
            coll.gameObject.SetActive(false);
 
            coll.gameObject.GetComponent<FireWeapon>().setInUse(true);
            //equipped = true;

            if(weapons.Count == maxWeapons) slotFull = true;
                //Make weapon a child of the camera and move it to default position
                coll.transform.SetParent(container);
                coll.transform.localPosition = Vector3.zero;
                coll.transform.localRotation = Quaternion.Euler(Vector3.zero);
                coll.transform.localScale = Vector3.one;

                //Make Rigidbody kinematic and BoxCollider a trigger
                coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                coll.isTrigger = true;

                //Enable script
                coll.gameObject.GetComponent<FireWeapon>().enabled = true;
                coll.enabled = false;
    }

    /*void DropWeapon(){

         // DROP WEAPONS
         // So let's say you wanted to add a feature where you wanted to drop the weapon you carry in your hand
         if (Input.GetKeyDown(dropKey) && currentWeapon != null) {
 
             // First ensure we remove our hand as parent for the weapon
             currentWeapon.transform.parent = null;
 
             // Move the weapon to the drop position
             currentWeapon.transform.position = dropPoint.position;
 
             // Remove it from our 'inventory'            
             var weaponInstanceId = currentWeapon.GetInstanceID();
             for (int i = 0; i < weapons.Count; i++) {
                 if (weapons[i].GetInstanceID() == weaponInstanceId) {
                     weapons.RemoveAt(i);
                     break;
                 }
             }
 
             // Remove it from our 'hand'
             currentWeapon = null;
         }
    }*/
}