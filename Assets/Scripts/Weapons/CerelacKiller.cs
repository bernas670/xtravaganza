using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerelacKiller : MonoBehaviour
{   
     [SerializeField] bool isPlayer = false;
     [SerializeField] float fireRate = 1f;
     [SerializeField] float damage = 10f;
     [SerializeField] float range = 10000f;
     [SerializeField] Camera cam;
     [SerializeField] GameObject enemy;
    private float timeToFire = 0f;

    void Update(){
        if(isPlayer){
            if (Input.GetButton("Fire1") && Time.time >= timeToFire){
                timeToFire = Time.time + 1f/fireRate;
                PlayerShoot();
            }
        }
        else if (!isPlayer){
            if(Time.time>=timeToFire)
            {   
                timeToFire = Time.time + 1f/fireRate;
                EnemyShoot();
            }
        }

    }

    void PlayerShoot(){
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
            Debug.Log(hit.transform.name);
        }
    }
    void EnemyShoot(){
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position - cam.transform.position, enemy.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            Debug.DrawRay(enemy.transform.position - cam.transform.position, cam.transform.forward, Color.red, 2);
        }
    }
}
