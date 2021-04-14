/*
using UnityEngine;

/* This class is responsible for keeping track of all stats of a character (health, damage, ..).
Goal is to derive from it and change methods in order to create game logic for all characters 
public class Weapon : MonoBehaviour
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    ShootingStrategy ShootStrat;
    private float timeToFire = 0f;


    void Update(){
        ShootStrat.Shoot();
    }

}
public interface ShootingStrategy {
     void Shoot();
}



public class PlayerStrategy : MonoBehaviour, ShootingStrategy {
    
    public void Shoot(){
        if (Input.GetButton("Fire1") && Time.time >= timeToFire){
            timeToFire = Time.time + 1f/fireRate;
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
            }
        }
    }
}

public class EnemyStrategy : MonoBehaviour, ShootingStrategy {
    public void Shoot(){
        if (Time.time >= timeToFire){
            timeToFire = Time.time + 1f/fireRate;
            RaycastHit hit;
            if(Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
            }
        }
    }
}
*/
