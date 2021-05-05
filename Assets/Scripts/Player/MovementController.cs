using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MovementController : MonoBehaviour
{

    private CharacterController _controller;
    private CameraController _camController;
    public Rigidbody rb;

    // temporary for debug purposes
    public TextMeshProUGUI horizontalSpeedText, verticalSpeedText;

    private const float MAX_SPEED = 15f;

    private const float GROUND_MAX_SPEED = 15f;
    private const float GROUND_ACCEL = 10f * GROUND_MAX_SPEED;
    private const float GROUND_FRICTION = 3f * GROUND_MAX_SPEED;

    private const float AIR_MAX_SPEED = 1.5f;

    private const float JUMP_HEIGHT = 2f;

    // 
    private bool _isGrounded = false;
    public bool wishJump = false;
    public Vector3 wishDir = Vector3.zero;

    private float _hVel = 0f;
    private float _vVel = 0f;


    // wall running vars
    private bool _leftWall = false;
    private bool _rightWall = false;
    private bool _isWallRunning = false;

    private float _wallDist = 0.8f;
    private float _minWallrunHeight = JUMP_HEIGHT - 0.5f;
    private float _wallRunGravity = -Physics.gravity.y * 0.3f;
    private float _wallRunJumpForce = 6f;
    private float _wallRunCamTilt = 20;

    RaycastHit _leftWallHit, _rightWallHit;

    private StateMachine _movementSM;


    void Start()
    {
        _camController = GetComponent<CameraController>();
        _controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();


        _movementSM = new StateMachine();

        GroundState ground = new GroundState(this, _movementSM);
        _movementSM.Initialize(ground);
    }

    void Update()
    {
        wishDir = Vector3.zero;
        wishDir.x = Input.GetAxis("Horizontal");
        wishDir.z = Input.GetAxis("Vertical");
        wishDir = transform.TransformDirection(wishDir);




        QueueJump();

        // FIXME: REMOVE, temporary for debug purposes
        horizontalSpeedText.text = string.Format("hspeed: {0}", _hVel.ToString("#.00"));
        verticalSpeedText.text = string.Format("vspeed: {0}", _vVel.ToString("#.00"));
    }

    void FixedUpdate()
    {


        // update velocity values
        _hVel = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
        _vVel = rb.velocity.y;

        // FIXME: REMOVE, display _velocity and _wishDir vectors
        // Debug.DrawLine(transform.position, transform.position + _rb.velocity, Color.green);
        // Debug.DrawLine(transform.position, transform.position + _wishDir, Color.blue);


        RaycastHit hitInfo;
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hitInfo, 1.1f);
         // draw ray
        // Debug.DrawLine(transform.position, transform.position - Vector3.up * 1.1f, Color.red);



        if (_isGrounded)
        {
            GroundMovement(Time.fixedDeltaTime);
        }
        else if (CanWallRun())
        {
            if(_isWallRunning)
                WallRun();
            else
                StartWallRun();
        }
        else if(_isWallRunning)
        {
            StopWallRun();
        }
        else
        {
            AirMovement(Time.fixedDeltaTime);
        }

        _movementSM.PhysicsUpdate();
    }


    void QueueJump()
    {
        if (Input.GetButton("Jump"))
            wishJump = true;
        if (Input.GetButtonUp("Jump"))
            wishJump = false;
    }

    void GroundMovement(float deltaTime)
    {
        Accelerate(GROUND_MAX_SPEED, GROUND_ACCEL, deltaTime);

        if (wishJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(JUMP_HEIGHT * 2f * -Physics.gravity.y), rb.velocity.z);
            wishJump = false;
        }
        else
        {
            ApplyFriction(GROUND_FRICTION, GROUND_MAX_SPEED, deltaTime);
        }

    }

    void AirMovement(float deltaTime)
    {
        Accelerate(AIR_MAX_SPEED, GROUND_ACCEL, deltaTime);
    }


    public void Accelerate(float maxSpeed, float acceleration, float deltaTime)
    {
        Vector3 vel = rb.velocity;
        vel.y = 0;

        float currentSpeed = Vector3.Dot(vel, wishDir);
        float addSpeed = Mathf.Clamp(maxSpeed - currentSpeed, 0, acceleration * deltaTime);

        rb.AddForce(wishDir * addSpeed, ForceMode.VelocityChange);
    }

    public void ApplyFriction(float friction, float maxSpeed, float deltaTime)
    {
        float drop = friction * deltaTime;
        float speed = Mathf.Clamp(rb.velocity.magnitude - drop, 0, maxSpeed);

        Vector3 velocity = rb.velocity.normalized * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void CheckWall()
    {
        _rightWall = Physics.Raycast(transform.position, transform.right, out _rightWallHit, _wallDist);
        _leftWall = Physics.Raycast(transform.position, -transform.right, out _leftWallHit, _wallDist);

        // FIXME: REMOVE, draw rays
        Debug.DrawLine(transform.position, transform.position + transform.right * _wallDist, Color.red);
        Debug.DrawLine(transform.position, transform.position - transform.right * _wallDist, Color.red);
    }

    public bool CanWallRun()
    {
        CheckWall();

        Debug.DrawLine(transform.position, transform.position + Vector3.down * _minWallrunHeight, Color.red);
        return !Physics.Raycast(transform.position, Vector3.down, _minWallrunHeight) && (_leftWall || _rightWall);
    }

    void StartWallRun()
    {
        rb.useGravity = false;
        _isWallRunning = true;
    }
    
    void StopWallRun()
    {
        rb.useGravity = true;
        _isWallRunning = false;
        StartCoroutine(_camController.AsyncTilt(0));
    }

    void WallRun()
    {
        rb.AddForce(Vector3.down * _wallRunGravity, ForceMode.Force);

        float tiltFactor = _leftWall ? -1 : 1;
        _camController.Tilt(tiltFactor * _wallRunCamTilt);

        if (Input.GetButtonDown("Jump"))
        {
            // Debug.Log("Tried to jump");
            if (!_leftWall && !_rightWall) return;

            // Debug.Log("Jumped");
            RaycastHit wall = _leftWall ? _leftWallHit : _rightWallHit;
            Vector3 direction = transform.up + wall.normal;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            float force = _wallRunJumpForce * Mathf.Clamp(_hVel * 5, 50, 150);
            // Debug.Log(force);
            rb.AddForce(direction * force, ForceMode.Force);
        }
    }

}
