using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This will contain the State Machine for the Enemy actions.

public class EnemyController : Shooter
{
    private NavMeshAgent _agent;
    // Contains the locations where the enemie can go.
    [SerializeField] List<Transform> _wayPointList;
    [HideInInspector] public int nextWayPoint;

    private Transform _target;
    public GameObject _player;
    [SerializeField] float lookRadius;

    void Awake(){
        lookRadius = 10f;
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
        /*
        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= lookRadius)
        {
            _agent.SetDestination(_target.position); //move towards target
            faceTarget(); //face target

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
        */
        faceTarget(); //face target
        UpdateState();

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

    public NavMeshAgent getAgent(){
        return _agent;
    }
    
    public List<Transform> getWayPointList(){
        return _wayPointList;
    }
    
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

                // attack
                AttackAction attack = new AttackAction();
                attack.Act(this);
                //chase;
                // Implement attacking decision (should I shoot or stab the player?);
                break;
       }
    }
}


