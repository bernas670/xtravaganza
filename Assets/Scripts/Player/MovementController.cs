using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovementController : MonoBehaviour
{

    private CharacterController _controller;
    private Rigidbody _rb;

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
    private bool _wishJump = false;
    private Vector3 _wishDir = Vector3.zero;

    private float _hVel = 0f;
    private float _vVel = 0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _wishDir = Vector3.zero;
        _wishDir.x = Input.GetAxis("Horizontal");
        _wishDir.z = Input.GetAxis("Vertical");
        _wishDir = transform.TransformDirection(_wishDir);

        QueueJump();

        // temporary for debug purposes
        horizontalSpeedText.text = string.Format("hspeed: {0}", _hVel.ToString("#.00"));
        verticalSpeedText.text = string.Format("vspeed: {0}", _vVel.ToString("#.00"));
    }

    void FixedUpdate()
    {
        // update velocity values
        _hVel = Mathf.Sqrt(Mathf.Pow(_rb.velocity.x, 2) + Mathf.Pow(_rb.velocity.z, 2));
        _vVel = _rb.velocity.y;

        // display _velocity and _wishDir vectors
        // Debug.DrawLine(transform.position, transform.position + _rb.velocity, Color.green);
        // Debug.DrawLine(transform.position, transform.position + _wishDir, Color.blue);

        RaycastHit hitInfo;
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hitInfo, 1.1f);
        // draw ray
        Debug.DrawLine(transform.position, transform.position - Vector3.up * 1.1f, Color.red);


        if (_isGrounded)
        {
            GroundMovement(Time.fixedDeltaTime);
        }
        else
        {
            AirMovement(Time.fixedDeltaTime);
        }
    }

    void QueueJump()
    {
        if (Input.GetButton("Jump"))
            _wishJump = true;
        if (Input.GetButtonUp("Jump"))
            _wishJump = false;
    }

    void GroundMovement(float deltaTime)
    {
        Accelerate(GROUND_MAX_SPEED, GROUND_ACCEL, deltaTime);

        if (_wishJump)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, Mathf.Sqrt(JUMP_HEIGHT * 2f * -Physics.gravity.y), _rb.velocity.z);
            _wishJump = false;
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

    void Accelerate(float maxSpeed, float acceleration, float deltaTime)
    {
        Vector3 vel = _rb.velocity;
        vel.y = 0;

        float currentSpeed = Vector3.Dot(vel, _wishDir);
        float addSpeed = Mathf.Clamp(maxSpeed - currentSpeed, 0, acceleration * deltaTime);

        _rb.AddForce(_wishDir * addSpeed, ForceMode.VelocityChange);
    }

    void ApplyFriction(float friction, float maxSpeed, float deltaTime)
    {
        float drop = friction * deltaTime;
        float speed = Mathf.Clamp(_rb.velocity.magnitude - drop, 0, maxSpeed);

        Vector3 velocity = _rb.velocity.normalized * speed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }

}
