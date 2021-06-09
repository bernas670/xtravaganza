using UnityEngine;

public class GroundState : MovementState
{
    private Rigidbody _rb;
    private const float GROUND_MAX_SPEED = 15f;
    private const float GROUND_ACCEL = 10f * GROUND_MAX_SPEED;
    private const float GROUND_FRICTION = 3f * GROUND_MAX_SPEED;
    private const float JUMP_HEIGHT = 4f;

    private bool _wishJump = false;

    public GroundState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _rb = _controller.rb;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        // Previous QueueJump
        if (Input.GetButton("Jump"))
            _wishJump = true;
        if (Input.GetButtonUp("Jump"))
            _wishJump = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_controller.IsGrounded()) {
            // Debug.Log("Ground -> Air");
            _sm.ChangeState(new AirState(_controller, _sm));
            return;
        }

        _controller.Accelerate(GROUND_MAX_SPEED, GROUND_ACCEL, Time.fixedDeltaTime);

        if (_wishJump) {
            _wishJump = false;
            _rb.velocity = new Vector3(_rb.velocity.x, Mathf.Sqrt(JUMP_HEIGHT * 2f * -Physics.gravity.y), _rb.velocity.z);
            // Debug.Log("Ground -> Air");
            _sm.ChangeState(new AirState(_controller, _sm));
            return;
        }

        _controller.ApplyFriction(GROUND_FRICTION, GROUND_MAX_SPEED, Time.fixedDeltaTime);
    }
}