using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

// This will contain the State Machine for the Enemy actions.

public class EnemyController : Shooter
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _target;
    public GameObject _player;

    // Contains the locations where the enemie can go.
    [SerializeField] List<Transform> _wayPointList;
    [HideInInspector] public int nextWayPoint;

    private bool _isEvil;
    private bool _isDead = false;
    private bool _isChasing = false;

    public GameObject rightHand;
    public GameObject leftHand;
    public MeshCollider collider;

    private float detectionRange = 50f;

    void Awake()
    {
        _isEvil = Random.Range(0, 2) == 0;
        setPoV(this.transform);

        _animator = GetComponent<Animator>();
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

        float velX = Vector3.Dot(_agent.velocity.normalized, transform.right);
        float velZ = Vector3.Dot(_agent.velocity.normalized, transform.forward);

        if (!_isChasing)
        {
            velX /= 1.5f;
            velZ /= 1.5f;
        }

        _animator.SetFloat("VelocityX", velX, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityZ", velZ, 0.1f, Time.deltaTime);
    }

    // State Machine
    public void UpdateState()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (_isDead)
        {            
            clearRigWeaponReference();
            DyingAction dying = new DyingAction();
            dying.Act(this);
        }
        else if (!fireWeapon || distance > detectionRange)
        {
            //patrol;   
            //Change the patrolling points created in Scene;
            _isChasing = false;
            fireWeapon.StopShootSound();
            PatrolAction patrol = new PatrolAction();
            patrol.Act(this);
            //  ------------TODO----------------------------
            //Use raycast, if player is in sight, chase him.

        }
        else
        {
            _isChasing = true;
            ChaseAction chase = new ChaseAction();
            chase.Act(this);
            // Choose the attack method
            FactoryAttackAction fact = new FactoryAttackAction();
            AttackAction action = fact.createAttackAction(this, distance);
            action.Act(this);
        }

    }

    // Getters
    public NavMeshAgent getAgent()
    {
        return _agent;
    }

    public Transform getTarget()
    {
        return _target;
    }

    public List<Transform> getWayPointList()
    {
        return _wayPointList;
    }

    public void dropWeapon()
    {
        fireWeapon.StopShootSound();
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

    public void CommunicateDeath()
    {
        dropWeapon();
        _isDead = true;
        _animator.SetTrigger("isDead");
        collider.enabled = false;
    }

    // detach the weapon from player;
    void clearRigWeaponReference()
    {
        rightHand.GetComponent<TwoBoneIKConstraint>().weight = 0;
        leftHand.GetComponent<TwoBoneIKConstraint>().weight = 0;
        rightHand.GetComponent<TwoBoneIKConstraint>().data.target = null;
        leftHand.GetComponent<TwoBoneIKConstraint>().data.target = null;
    }
}