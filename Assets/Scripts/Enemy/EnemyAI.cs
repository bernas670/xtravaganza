using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] float lookRadius = 10f;

    void Start()
    {
        _target = GameObject.Find("Player").transform; /* Make this a singleton + game manager*/
        _agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if(distance <= lookRadius){
            _agent.SetDestination(_target.position); /*move towards target*/

            if(distance <= _agent.stoppingDistance){
                faceTarget(); /*face target*/
            }
        }
    }

    void faceTarget(){
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7.5f);
    }
}
