using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This will contain the State Machine for the Enemy actions.

public class EnemyController : Shooter
{
    private NavMeshAgent _agent;
    private Transform _target;
    public GameObject _player;

    // Contains the locations where the enemie can go.
    [SerializeField] List<Transform> _wayPointList;
    [HideInInspector] public int nextWayPoint;

    private bool _isEvil;

    void Awake(){
        _isEvil = Random.Range(0,2) == 0;
    }
    void Start()
    {
        _player = GameObject.Find("Player"); /* Make this a singleton + game manager*/
        _target = _player.transform; /* Make this a singleton + game manager*/
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {       
        UpdateState();
    }

    // State Machine
    public void UpdateState()
    {   
       float distance = Vector3.Distance(_target.position, transform.position);
       if (distance > fireWeapon.getRange()){
            //patrol;   
            //Change the patrolling points created in Scene;
            PatrolAction patrol = new PatrolAction();
            patrol.Act(this);
            //  ------------TODO----------------------------
            //Use raycast, if player is in sight, chase him.

       }else{
            //if(!_isEvil) break;
            ChaseAction chase = new ChaseAction();
            chase.Act(this);
            // Choose the attack method
            FactoryAttackAction fact = new FactoryAttackAction();
            AttackAction action = fact.createAttackAction(this, distance);
            action.Act(this);
       }
       
    }

    // Getters
    public NavMeshAgent getAgent(){
        return _agent;
    }

    public Transform getTarget(){
        return _target;
    }
    
    public List<Transform> getWayPointList(){
        return _wayPointList;
    }
}


