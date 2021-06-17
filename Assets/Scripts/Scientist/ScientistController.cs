using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ScientistController : MonoBehaviour
{
    public GameObject _player;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Scientist _scientist;

    // Contains the locations where the enemie can go.
    [SerializeField] List<Transform> _wayPointList;
    [HideInInspector] public int nextWayPoint;

    void Start()
    {
        _player = GameObject.Find("Player"); // Make this a singleton + game manager
        _agent = GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _scientist = gameObject.GetComponent<Scientist>();

        
    }

    void Update()
    {       
        if (!_player)
        {
            _player = GameObject.Find("Player");
        }
        
        if(_scientist.isDead()){
            _agent.Stop(); //stops scientist from running away
            return;
        }
        
        float distanceToPlayer = Vector3.Distance( transform.position, _player.transform.position);

        if(distanceToPlayer > 40f && distanceToPlayer < 100f){
            _agent.Resume();

            _animator.SetBool("isRunning", false);
            _animator.SetBool("isSafe", true);

            _agent.destination = _wayPointList[nextWayPoint].position;
            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending && !_scientist.isDead()) 
            {
                nextWayPoint = (nextWayPoint + 1) % _wayPointList.Count;
            }
        }
        else if(distanceToPlayer < 40f && distanceToPlayer > 20f) {
            _agent.Stop(); //stops scientist from running away

            _animator.SetTrigger("isTerrified");
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isSafe", false);

        } else if(distanceToPlayer < 20f) {  
            _agent.Resume();
            
            _animator.SetBool("isSafe", false); 
            _animator.SetBool("isRunning", true);
            
            RunFromAlien();
        }
        
    }
    
    public void communicateDeath(){
        _animator.SetTrigger("isDead");
        Player player = _player.GetComponent<Player>();
        player.becomeEvil();
    }

    public void RunFromAlien()
    {
         // store the starting transform
        Transform startTransform = transform;
        Vector3 startVelocity = _rigidbody.velocity;
        Vector3 startAngVelocity = _rigidbody.angularVelocity;

        //temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - _player.transform.position);

         //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
         // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + transform.forward * 10;
         //Debug.Log("runTo = " + runTo);

         //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.
        NavMeshHit hit;    // stores the output in a variable called hit

         // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Walkable")); 
         //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

         // reset the transform back to our start transform
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
 
         // And get it to head towards the found NavMesh position
        _agent.SetDestination(hit.position);
     }

     private void resetVelocity(Vector3 startVel, Vector3 angVel){
        _rigidbody.velocity = startVel;
        _rigidbody.angularVelocity = angVel;
     }
}