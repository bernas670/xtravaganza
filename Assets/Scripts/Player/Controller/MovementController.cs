using UnityEngine;
using TMPro;

public class MovementController : MonoBehaviour
{
    public Animator animator;
    private CameraController _camController;
    [HideInInspector]
    public Rigidbody rb;

    // temporary for debug purposes
    public TextMeshProUGUI horizontalSpeedText, verticalSpeedText;

    private float WALL_DIST = 1.6f;
    private float MAX_GROUND_DIST = 1.1f;
    private float MIN_WALL_RUN_HEIGHT = 1.5f;
    private Vector3 _wishDir = Vector3.zero;

    private float _hVel = 0f;
    private float _vVel = 0f;

    private StateMachine _movementSM;

    void Start()
    {
        _camController = GetComponent<CameraController>();
        rb = GetComponent<Rigidbody>();

        _movementSM = new StateMachine();
        GroundState ground = new GroundState(this, _movementSM);
        _movementSM.Initialize(ground);
    }

    void Update()
    {
        _wishDir = Vector3.zero;
        _wishDir.x = Input.GetAxis("Horizontal");
        _wishDir.z = Input.GetAxis("Vertical");
        _wishDir = transform.TransformDirection(_wishDir);
        _wishDir.Normalize();

        //Debug.Log("Wish dir X = " + zCoef);
        //Debug.Log("Wish dir Z = " + xCoef);
        //Debug.Log((_hVel * xCoef) * 100 / 15);

        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float zVelocity = Vector3.Dot(velocity.normalized, transform.forward);
        float xVelocity = Vector3.Dot(velocity.normalized, transform.right);

        animator.SetFloat("zVelocity", zVelocity);
        animator.SetFloat("xVelocity", xVelocity);

        _movementSM.HandleInput();

        // FIXME: REMOVE, temporary for debug purposes
        horizontalSpeedText.text = string.Format("hspeed: {0}", _hVel.ToString("#.00"));
        verticalSpeedText.text = string.Format("vspeed: {0}", _vVel.ToString("#.00"));
    }

    void FixedUpdate()
    {
        _hVel = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
        _vVel = rb.velocity.y;

        _movementSM.PhysicsUpdate();
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, MAX_GROUND_DIST);
    }

    public int GetWallRunFactor(out RaycastHit hit) {
        RaycastHit left, right;
        bool rightWall = Physics.Raycast(transform.position, transform.right, out right, WALL_DIST);
        bool leftWall = Physics.Raycast(transform.position, -transform.right, out left, WALL_DIST);

        if(leftWall && rightWall) {
            hit = new RaycastHit();
            return 0;
        }

        if(leftWall) {
            hit = left;
            return -1;
        }
        
        hit = right;
        return 1;
    }
    
    public int GetWallRunFactor() {
        bool rightWall = Physics.Raycast(transform.position, transform.right, WALL_DIST);
        bool leftWall = Physics.Raycast(transform.position, -transform.right, WALL_DIST);

        if(leftWall && rightWall) {
            return 0;
        }

        if(leftWall) {
            return -1;
        }
        
        return 1;
    }

    public bool CanWallRun()
    {
        bool rightWall = Physics.Raycast(transform.position, transform.right, WALL_DIST);
        bool leftWall = Physics.Raycast(transform.position, -transform.right, WALL_DIST);
        
        return !Physics.Raycast(transform.position, Vector3.down, MIN_WALL_RUN_HEIGHT) && (leftWall || rightWall);
    }

    public void Accelerate(float maxSpeed, float acceleration, float deltaTime)
    {
        Vector3 vel = rb.velocity;
        vel.y = 0;

        float currentSpeed = Vector3.Dot(vel, _wishDir);
        float addSpeed = Mathf.Clamp(maxSpeed - currentSpeed, 0, acceleration * deltaTime);

        rb.AddForce(_wishDir * addSpeed, ForceMode.VelocityChange);
    }

    public void ApplyFriction(float friction, float maxSpeed, float deltaTime)
    {
        float drop = friction * deltaTime;
        float speed = Mathf.Clamp(rb.velocity.magnitude - drop, 0, maxSpeed);

        Vector3 velocity = rb.velocity.normalized * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    public void Tilt(float target, bool async = false) {
        if(async) {
            StartCoroutine(_camController.AsyncTilt(target));
            return;
        }

        _camController.Tilt(target);
    }


}
