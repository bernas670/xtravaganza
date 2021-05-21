using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{   
    public int selectedWeapon = 0;
    public Player player;
    private PlayerShootController _shooter;
    void Start(){
        _shooter = player.gameObject.GetComponent<PlayerShootController>();
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
}