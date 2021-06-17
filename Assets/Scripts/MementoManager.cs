using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoManager : MonoBehaviour
{
    private SnapshotPlayer _snapshotPlayer;

    public void Awake()
    {
        _snapshotPlayer = null;
    }
    public void CreateSnapshot(Player player, WeaponSwitchController weaponSwitchController)
    {
        _snapshotPlayer = new SnapshotPlayer(player.getHealthStat().getHealth(), weaponSwitchController.weapons);
    }

    public SnapshotPlayer GetSnapshot()
    {
        return _snapshotPlayer;
    }
}
