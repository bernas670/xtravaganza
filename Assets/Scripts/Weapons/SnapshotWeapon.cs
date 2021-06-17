using UnityEngine;

public class SnapshotWeapon
{
    public int currentAmmo;
    public int maxAmmo;
    public GameObject prefab;

    public SnapshotWeapon(int cAmmo, int mAmmo, GameObject prefab)
    {
        this.currentAmmo = cAmmo;
        this.maxAmmo = mAmmo;
        this.prefab = prefab;
    }
}
