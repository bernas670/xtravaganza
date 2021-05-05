using UnityEngine;

public class AirState : MovementState
{

    private const float AIR_MAX_SPEED = 1.5f;
    private const float AIR_ACCEL = 150f;


    public AirState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

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

        // check for ground collision
        if (Physics.Raycast(_controller.transform.position, -Vector3.up, 1.1f)) {
            _sm.ChangeState(new GroundState(_controller, _sm));
        }
        // check for walls
        else if (_controller.CanWallRun()) {
            _sm.ChangeState(new WallRunningState(_controller, _sm));
        }
        else {
            _controller.Accelerate(AIR_MAX_SPEED, AIR_ACCEL, Time.fixedDeltaTime);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}