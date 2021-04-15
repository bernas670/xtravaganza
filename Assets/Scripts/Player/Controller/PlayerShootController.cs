using UnityEngine;

public class PlayerShootController : Shooter
{
    public void Awake(){
        fireRate = 1f;
        timeToFire= 0f;
    }
    void Update()
    {
        shoot();
    }

    public override void shoot()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.getRange()))
            {
                if (hit.transform.name == "Enemy")
                {
                    Debug.Log("Player Shot");
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.getHealth().TakeDamage(weapon.getDamage());
                }
            }
        }
    }
}