using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WeaponUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponsList;

    public void SetAmmo(int clip, int ammo)
    {
        ammoText.text = string.Format("{0} / {1}", clip, ammo);
    }

    public void SetWeaponName(string name)
    {
        weaponNameText.text = name;
    }

    public void updateWeaponsList(List<GameObject> weapons, int currentIndex)
    {
        weaponsList.text = "";

        if (weapons.Count == 0)
        {
            weaponsList.text = "Empty bag";
        }

        for (int i = weapons.Count - 1; i >= 0; i--)
        {
            int j = (i + currentIndex) % weapons.Count;
            if (j == currentIndex) continue;

            weaponsList.text += weapons[j].name + "\n";
        }
    }
}
