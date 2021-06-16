using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI weaponNameText;

    public void SetAmmo(int clip, int ammo)
    {
        ammoText.text = string.Format("{0} / {1}", clip, ammo);
    }

    public void SetWeaponName(string name)
    {
        weaponNameText.text = name;
    }
}
