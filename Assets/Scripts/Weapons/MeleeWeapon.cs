using UnityEngine;
public abstract class MeleeWeapon : Weapon
{
    public override void shoot(Shooter controller)
    {
        if (Time.time >= _timeToFire)
        {
            setTimeToFire(Time.time + 1f / _fireRate);
            RaycastHit hit;
            if (Physics.Raycast(controller.getPoV().position, controller.getPoV().forward, out hit, _range))
            {
                if (hit.transform.name == "Player") /* controller = enemy */
                {
                    Player player = hit.transform.gameObject.GetComponent<Player>();
                    player.TakeDamage(_damage);
                }
                else if (hit.transform.name == "Enemy") /* controller = player || enemy */
                {
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}
