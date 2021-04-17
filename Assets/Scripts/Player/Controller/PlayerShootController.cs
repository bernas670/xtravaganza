using UnityEngine;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;

    void Update()
    {   
        if(Input.GetButton("Fire1")){
            setWeapon(cerelac);
            shoot();
        }
        else if(Input.GetButton("Fire2")){
            setWeapon(dagger);
            shoot();
        }
    }
    public void shoot()
    {   
        Debug.Log("Player Shot : " + weapon);
        if (Time.time >= weapon.getTimeToFire())
        {       
            weapon.setTimeToFire( Time.time + 1f / weapon.getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.getRange()))
            {
                if (hit.transform.name == "Enemy")
                {
                    Debug.Log("Player Shot");
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(weapon.getDamage());
                }
            }
        }
    }
}