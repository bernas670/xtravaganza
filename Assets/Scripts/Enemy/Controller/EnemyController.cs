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
    [SerializeField] float lookRadius;

    // Contains the locations where the enemie can go.
    [SerializeField] List<Transform> _wayPointList;
    [HideInInspector] public int nextWayPoint;

    private bool _isEvil;

    void Awake(){
        lookRadius = 10f;
        _isEvil = Random.Range(0,2) == 0;
        // !!!!!!!!!!
        // This setWeapon is temporary
        setWeapon(cerelac);
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

    void faceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7.5f);
    }  

    // State Machine
    public void UpdateState()
    {   
       float distance = Vector3.Distance(_target.position, transform.position);
       switch (distance > weapon.getRange()) {
           case (true):
                //patrol;   
                //Change the patrolling points created in Scene;
                PatrolAction patrol = new PatrolAction();
                patrol.Act(this);
                //Use raycast, if player is in sight, chase him.
                break;
            case (false):

                if(!_isEvil) break;

                faceTarget();
                // attack
                AttackAction attack = new AttackAction();
                attack.Act(this);

                break;
       }
    }

    // Getters
    public NavMeshAgent getAgent(){
        return _agent;
    }
    
    public List<Transform> getWayPointList(){
        return _wayPointList;
    }
}


