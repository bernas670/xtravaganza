using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;

    private GameObject _player;
    private PlayerStat _playerStat;
    private CerelacKiller _cerelac;

    [SerializeField] float lookRadius = 10f;

    void Start()
    {   
        _player = GameObject.Find("Player"); /* Make this a singleton + game manager*/
        _target = _player.transform; /* Make this a singleton + game manager*/
        _playerStat = _player.GetComponent<PlayerStat>();
        _agent = GetComponent<NavMeshAgent>();
        _cerelac = GetComponent<CerelacKiller>();
        
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if(distance <= lookRadius){
            _agent.SetDestination(_target.position); /*move towards target*/

            if(distance <= _agent.stoppingDistance){
                faceTarget(); /*face target*/
                meleeAttack();
            }
            else {
                _cerelac.EnemyShoot();         
                }
        }
    }

    void faceTarget(){
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7.5f);
    }


    void meleeAttack(){
        //Debug.Log("Melee");
        //_playerStat.TakeDamage(10);
    }

    void shootAttack(){
        //Debug.Log("Shoot");
        //_playerStat.TakeDamage(5);
    }
}

