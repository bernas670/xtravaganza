using UnityEngine;

public class WallRunningState : MovementState
{

    private bool _jump = false;
    private float _wallRunGravity = -Physics.gravity.y * 0.3f;


    public WallRunningState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        _jump = Input.GetButtonDown("Jump");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _controller.rb.AddForce(Vector3.down * _wallRunGravity, ForceMode.Force);

        if (!_jump) return;

    }
}