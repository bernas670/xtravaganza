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
        _agent.destination = getWayPointList() [nextWayPoint].position;
        //_agent.isStopped = false;

        if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending) 
        {
            nextWayPoint = (nextWayPoint + 1) % getWayPointList().Count;
        }
    }

    public void UpdateState()
    {   
        if( Vector3.Distance( transform.position, _player.transform.position) < 20f) {
            _animator.SetBool("isRunning", true);
            //path finding system 
        } else {
            //scientist feels terrified

        }

        // work (walk)
    }

    // Getters
    public NavMeshAgent getAgent(){
        return _agent;
    }

    public Transform getTarget(){
        return _player.transform;
    }
    
    public List<Transform> getWayPointList(){
        return _wayPointList;
    }

    public void communicateDeath(){
        Player player = _player.GetComponent<Player>();
        player.becomeEvil();
    }
}