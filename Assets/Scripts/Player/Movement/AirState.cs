using UnityEngine;

public class AirState : MovementState
{
    private Transform _transform;
    private const float AIR_MAX_SPEED = 1.5f;
    private const float AIR_ACCEL = 150f;

    private float _wallDist = 0.8f;
    private float _minWallrunHeight = 1.5f;

    public AirState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _transform = _controller.transform;
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

        if (_controller.IsGrounded()) {
            Debug.Log("Air -> Ground");
            _sm.ChangeState(new GroundState(_controller, _sm));
            return;
        }
        
        if (CanWallRun()) {
            Debug.Log("Air -> WallRun");
            _sm.ChangeState(new WallRunningState(_controller, _sm));
            return;
        }
        
        _controller.Accelerate(AIR_MAX_SPEED, AIR_ACCEL, Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }

    bool CanWallRun()
    {
        bool _rightWall = Physics.Raycast(_transform.position, _transform.right, _wallDist);
        bool _leftWall = Physics.Raycast(_transform.position, -_transform.right, _wallDist);
        
        return !Physics.Raycast(_transform.position, Vector3.down, _minWallrunHeight) && (_leftWall || _rightWall);
    }
}