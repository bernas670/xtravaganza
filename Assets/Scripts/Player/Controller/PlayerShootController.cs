using UnityEngine;

public class PlayerShootController : Shooter
{
    [SerializeField] protected Camera cam;
    
    void Awake(){
        setPoV(cam.transform);
    }

    void Update()
    {   

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 hitPoint;
        if (Physics.Raycast(ray, out hit))
        {
            hitPoint = ray.GetPoint(hit.distance);
        }
        else
        {
            hitPoint = ray.GetPoint(100/* or some very far distance */);
        }
    

        if(Input.GetButton("Fire1") && fireWeapon.getClipValue()>0){
            fireWeapon.shoot(this);
        }
        else if(Input.GetButton("Fire2")){ //TODO: check distances
            meleeWeapon.shoot(this);
        }
        else if(Input.GetKey(KeyCode.R)){
            fireWeapon.reload();
        }
    }

}