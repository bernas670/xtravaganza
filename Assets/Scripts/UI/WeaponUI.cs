using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class WeaponUI : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI weaponNameText;

    public TextMeshProUGUI weaponsList;

    public void SetAmmo(int clip, int ammo) {
        ammoText.text = string.Format("{0} / {1}", clip, ammo);
    }

    public void SetWeaponName(string name){
        weaponNameText.text = name;
    }
   public void updateWeaponsList(List<GameObject> weapons){
       weaponsList.text = "";
       if(weapons.Count == 0) weaponsList.text = "Empty bag";
        foreach (GameObject weapon in weapons){
            weaponsList.text += weapon.name + "\n";
        }
   }        
}
