using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerelacKiller : MonoBehaviour
{   
     [SerializeField] float fireRate = 1f;
     [SerializeField] int damage = 10;
     [SerializeField] float range = 100f;
     [SerializeField] Camera cam;

    private PlayerStat _playerStat;
    private EnemyStat _enemyStat;
    private float timeToFire = 0f;
    public void PlayerShoot(){
        if (Input.GetButton("Fire1") && Time.time >= timeToFire){
                Debug.Log("Player Shot");
                timeToFire = Time.time + 1f/fireRate;
                RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
                if(hit.transform.name == "Enemy")
                    _enemyStat = hit.transform.gameObject.GetComponent<EnemyStat>();
                    _enemyStat.TakeDamage(damage);
            }
        }
    }
    public void EnemyShoot(){
        if(Time.time>=timeToFire)
        {   
            Debug.Log("Enemy Shot");    
            timeToFire = Time.time + 1f/fireRate;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
                if(hit.transform.name == "Player")
                    _playerStat = hit.transform.gameObject.GetComponent<PlayerStat>();
                    _playerStat.TakeDamage(damage);
            }
        }
    }

}
