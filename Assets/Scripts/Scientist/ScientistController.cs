using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ScientistController : MonoBehaviour
{
    public GameObject _player;
    private NavMeshAgent _agent;

    private Animator _animator;

    // Contains the locations where the enemie can go.
    [SerializeField] List<Transform> _wayPointList;
    [HideInInspector] public int nextWayPoint;

    void Start()
    {
        _player = GameObject.Find("Player"); // Make this a singleton + game manager
        _agent = GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {       
        float distanceToPlayer = Vector3.Distance( transform.position, _player.transform.position);
        
        if(distanceToPlayer > 40f && distanceToPlayer < 100f){
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isSafe", true);


            _agent.destination = _wayPointList[nextWayPoint].position;
            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending) 
            {
                nextWayPoint = (nextWayPoint + 1) % _wayPointList.Count;

                /* TODO: stop between points */
            }
        }
        else if(distanceToPlayer < 40f && distanceToPlayer > 20f) {
            _animator.SetTrigger("isTerrified");
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isSafe", false);

        } else if(distanceToPlayer < 20f) {  
            _animator.SetBool("isSafe", false); 
            _animator.SetBool("isRunning", true);
        }
    }
    
    public void communicateDeath(){
        _animator.SetTrigger("isDead");

        Player player = _player.GetComponent<Player>();
        player.becomeEvil();
    }
}