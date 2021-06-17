using System.Collections.Generic;

public class SnapshotPlayer
{
    public int health;
    public List<SnapshotWeapon> weapons;

    public SnapshotPlayer(int health, List<SnapshotWeapon> weapons)
    {
        this.health = health;
        this.weapons = weapons;
    }
}
