using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{

    public TextMeshProUGUI ammoText;

    public void SetAmmo(int clip, int ammo) {
        ammoText.text = string.Format("{0} / {1}", clip, ammo);
    }
}
