using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Shooter
{
    private NavMeshAgent _agent;
    private Transform _target;
    private GameObject _player;
    [SerializeField] float lookRadius;

    void Awake(){
        lookRadius = 10f;
    }
        void Start()
    {
        _player = GameObject.Find("Player"); /* Make this a singleton + game manager*/
        _target = _player.transform; /* Make this a singleton + game manager*/
        _agent = GetComponent<NavMeshAgent>();


    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance <= lookRadius)
        {
            _agent.SetDestination(_target.position); /*move towards target*/
            faceTarget(); /*face target*/

            // ---->>>>>>   Should we just use dagger.range instead of agent stopping distance?
            if (distance <= _agent.stoppingDistance)
            {   
                // refactor this
                setWeapon(dagger);
                shoot();
            }
            else
            {   
                setWeapon(cerelac);
                shoot();
            }
        }
    }

    void faceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7.5f);
    }

    public override void shoot()
    {
        
        if (Time.time >= weapon.getTimeToFire())
        {
            weapon.setTimeToFire(Time.time + 1f / weapon.getFireRate());
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, weapon.getRange()))
            {
                if (hit.transform.name == "Player")
                {
                    Debug.Log("Enemy attacked");
                    Player _playerStat = _player.GetComponent<Player>();
                    _playerStat.TakeDamage(weapon.getDamage());
                }
            }
        }
    }
    
}


