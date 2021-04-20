using UnityEngine;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;
    

    void Update()
    {   
    
        if(Input.GetButton("Fire1") && fireWeapon.getClipValue()>0){
            shoot(fireWeapon);
        }
        else if(Input.GetButton("Fire2")){
            shoot(meleeWeapon);
        }
        else if(Input.GetKey(KeyCode.R)){
            fireWeapon.reload();
        }
    }
    public void shoot(Weapon weapon)
    {   
        Debug.Log("Player Shot : " + weapon);
        if (Time.time >= weapon.getTimeToFire())
        {    


            // !!!!!!!!!!! Need to separate Shoot and Stab actions;
            //  fireWeapon.decresaseAmmo();
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