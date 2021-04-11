using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovementController : MonoBehaviour
{

    private CharacterController _controller;

    // temporary for debug purposes
    public TextMeshProUGUI velocityText;


    private const float MAX_SPEED = 15f;

    private const float GROUND_MAX_SPEED = 15f;
    private const float GROUND_ACCEL = 100f * GROUND_MAX_SPEED;
    private const float GROUND_FRICTION = 1f * GROUND_MAX_SPEED;

    private const float AIR_MAX_SPEED = 1.5f;

    private const float JUMP_HEIGHT = 2f;
    private const float GRAVITY = 10f;

    // 
    private bool _wishJump = false;
    private Vector3 _wishDir = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;


    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // _inputs = Vector3.zero;
        _wishDir = Vector3.zero;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _wishDir = new Vector3(x, 0, z);
        _wishDir = transform.TransformDirection(_wishDir);

        QueueJump();

        // temporary for debug purposes
        velocityText.text = Mathf.Sqrt(Mathf.Pow(_velocity.x, 2) + Mathf.Pow(_velocity.z, 2)).ToString("#.00");
    }

    void FixedUpdate()
    {
        if (_controller.isGrounded)
        {
            GroundMovement(Time.fixedDeltaTime);
        }
        else
        {
            AirMovement(Time.fixedDeltaTime);
        }
        _controller.Move(_velocity * Time.fixedDeltaTime);
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

        // check if player intends to jump
        if (_wishJump)
        {
            _velocity.y = Mathf.Sqrt(JUMP_HEIGHT * 2f * GRAVITY);
            _wishJump = false;
        }
        else
        {
            ApplyFriction(GROUND_FRICTION, deltaTime);
        }
    }

    void AirMovement(float deltaTime)
    {
        Accelerate(AIR_MAX_SPEED, GROUND_ACCEL, deltaTime);

        // apply gravity
        _velocity.y -= GRAVITY * deltaTime;
    }

    void Accelerate(float maxSpeed, float acceleration, float deltaTime)
    {
        // prevent unnecessary operations
        if (_wishDir == Vector3.zero)
            return;

        // display _velocity and _wishDir vectors
        Debug.DrawLine(transform.position, transform.position + _velocity, Color.green);
        Debug.DrawLine(transform.position, transform.position + _wishDir, Color.blue);

        float currentSpeed = Vector3.Dot(_velocity, _wishDir);
        float addSpeed = Mathf.Clamp(maxSpeed - currentSpeed, 0, acceleration * deltaTime);

        _velocity += _wishDir * addSpeed;
    }

    // FIXME: not very responsive when player wants to stop
    void ApplyFriction(float friction, float deltaTime)
    {
        float drop = friction * deltaTime;
        float speed = Mathf.Clamp(_velocity.magnitude - drop, 0, MAX_SPEED);

        _velocity = _velocity.normalized * speed;
    }
}
