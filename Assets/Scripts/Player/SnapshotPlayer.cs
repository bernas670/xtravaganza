using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SnapshotPlayer
{
    public int health;
    public List<GameObject> weapons;
    public SnapshotPlayer(int health, List<GameObject> weapons)
    {
        this.health = health;
        this.weapons = weapons;
    }
}
