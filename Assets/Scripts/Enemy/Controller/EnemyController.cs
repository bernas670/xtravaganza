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


    void Awake(){
        setPoV(this.transform);
    }

    void Start()
    {
        _player = GameObject.Find("Player"); // Make this a singleton + game manager
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
       if (distance > fireWeapon.getRange() || !fireWeapon){
            //patrol;   
            //Change the patrolling points created in Scene;
            PatrolAction patrol = new PatrolAction();
            patrol.Act(this);
            //  ------------TODO----------------------------
            //Use raycast, if player is in sight, chase him.

       } else { 
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

    public void dropWeapon(){
        fireWeapon.setInUse(false);
        fireWeapon.gameObject.transform.parent = null;

        Rigidbody rb = fireWeapon.GetComponent<Rigidbody>();
        BoxCollider coll = fireWeapon.GetComponent<BoxCollider>();
 
        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = gameObject.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(transform.forward * 2, ForceMode.Impulse);
        rb.AddForce(transform.up * 3, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        fireWeapon.enabled = false;
        fireWeapon = null;
    }
}