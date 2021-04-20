using UnityEngine;
public abstract class MeleeWeapon : Weapon
{
    public override void shoot(Shooter controller)
    {
        if (Time.time >= controller.getFireWeapon().getTimeToFire())
        {
            controller.getFireWeapon().setTimeToFire(Time.time + 1f / controller.getFireWeapon().getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(controller.getPoV().position, controller.getPoV().forward, out hit, controller.getFireWeapon().getRange()))
            {
                if (hit.transform.name == "Player") /* controller = enemy */
                {
                    Debug.Log("MELEE: \t" + controller.gameObject.name + " attacked player");
                    Player player = hit.transform.gameObject.GetComponent<Player>();
                    player.TakeDamage(controller.getFireWeapon().getDamage());
                }
                else if (hit.transform.name == "Enemy") /* controller = player || enemy */
                {
                    Debug.Log("MELEE: \t" + controller.gameObject.name + " attacked enemy");
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(controller.getFireWeapon().getDamage());
                }
            }
        }
    }
}
