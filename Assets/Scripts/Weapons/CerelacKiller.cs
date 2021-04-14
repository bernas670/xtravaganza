using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerelacKiller : MonoBehaviour
{   
     [SerializeField] bool isPlayer = false;
     [SerializeField] float fireRate = 1f;
     [SerializeField] int damage = 10;
     [SerializeField] float range = 100f;
     [SerializeField] Camera cam;
     [SerializeField] GameObject player;
     [SerializeField] GameObject enemy;

    private PlayerStat _playerStat;
    private EnemyStat _enemyStat;
    private float timeToFire = 0f;

    void Start(){
        _playerStat = player.GetComponent<PlayerStat>();
        _enemyStat = enemy.GetComponent<EnemyStat>();

    }
    void Update(){
        if(isPlayer){
            Debug.DrawRay(cam.transform.position, cam.transform.forward.normalized * range, Color.green, 5f);
            
        }
        else if (!isPlayer){
            Debug.DrawRay(enemy.transform.position, enemy.transform.forward.normalized * range, Color.red, 5f);
        }
    }

    public void PlayerShoot(){
        if (Input.GetButton("Fire1") && Time.time >= timeToFire){
                Debug.Log("Player Shot");
                timeToFire = Time.time + 1f/fireRate;
                RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
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
            if(Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, range)){
                Debug.Log("hitttt");
                Debug.Log(hit.transform.name);
                _playerStat.TakeDamage(damage);
            }
        }
    }

}
