using UnityEngine;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;
    
    void Awake(){
        setPoV(cam.transform);
    }

    void Update()
    {   
    
        if(Input.GetButton("Fire1") && fireWeapon.getClipValue()>0){
            //shoot(fireWeapon);
            fireWeapon.shoot(this);
        }
        else if(Input.GetButton("Fire2")){ //check distances
            meleeWeapon.shoot(this);
        }
        else if(Input.GetKey(KeyCode.R)){
            fireWeapon.reload();
        }
    }

}