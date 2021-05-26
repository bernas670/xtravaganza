using UnityEngine;

public class AirState : MovementState
{
    private Transform _transform;
    private const float AIR_MAX_SPEED = 3.0f;
    private const float AIR_ACCEL = 150f;

    public AirState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _transform = _controller.transform;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_controller.IsGrounded()) {
            // Debug.Log("Air -> Ground");
            _sm.ChangeState(new GroundState(_controller, _sm));
            return;
        }
        
        if (_controller.CanWallRun()) {
            // Debug.Log("Air -> WallRun");
            _sm.ChangeState(new WallRunningState(_controller, _sm));
            return;
        }
        
        _controller.Accelerate(AIR_MAX_SPEED, AIR_ACCEL, Time.fixedDeltaTime);
    }
}