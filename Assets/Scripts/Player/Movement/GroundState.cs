using UnityEngine;

public class GroundState : MovementState
{

    private const float GROUND_MAX_SPEED = 15f;
    private const float GROUND_ACCEL = 10f * GROUND_MAX_SPEED;
    private const float GROUND_FRICTION = 3f * GROUND_MAX_SPEED;

    private const float JUMP_HEIGHT = 2f;


    public GroundState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _controller.Accelerate(GROUND_MAX_SPEED, GROUND_ACCEL, Time.fixedDeltaTime);

        Rigidbody rb = _controller.rb;

        // check for ground collision
        if (!Physics.Raycast(_controller.transform.position, -Vector3.up, 1.1f)) {
            _sm.ChangeState(new AirState(_controller, _sm));
        }
        else if (_controller.wishJump) {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(JUMP_HEIGHT * 2f * -Physics.gravity.y), rb.velocity.z);
            _controller.wishJump = false;
            _sm.ChangeState(new AirState(_controller, _sm));
        }
        else
        {
            _controller.ApplyFriction(GROUND_FRICTION, GROUND_MAX_SPEED, Time.fixedDeltaTime);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}