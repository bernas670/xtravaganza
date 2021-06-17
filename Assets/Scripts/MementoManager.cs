using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoManager : MonoBehaviour
{
    private SnapshotPlayer _snapshotPlayer;
    public List<string> scriptNames;
    public List<GameObject> prefabs;

    public void Awake()
    {
        _snapshotPlayer = null;
    }

    public void CreateSnapshot(Player player, WeaponSwitchController weaponSwitchController)
    {
        List<SnapshotWeapon> weapons =  new List<SnapshotWeapon>();
        foreach (GameObject weaponObj in weaponSwitchController.weapons)
        {
            FireWeapon weapon = weaponObj.GetComponent<FireWeapon>();
            int index = scriptNames.IndexOf(weapon.GetType().Name);
            weapons.Add(new SnapshotWeapon(weapon.getClipValue(), weapon.getReloadValue(), prefabs[index]));
        }
        _snapshotPlayer = new SnapshotPlayer(player.getHealthStat().getHealth(), weapons);
    }

    public SnapshotPlayer GetSnapshot()
    {
        return _snapshotPlayer;
    }

    public void ClearSnapshot()
    {
        _snapshotPlayer = null;
    }
}
