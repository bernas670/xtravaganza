using UnityEngine;

public class WallRunningState : MovementState
{
    private Transform _transform;
    private Rigidbody _rb;
    private bool _jump = false;

    private float _wallDist = 0.8f;
    private float _minWallrunHeight = 1.5f;
    private float _wallRunGravity = -Physics.gravity.y * 0.3f;
    private float _wallRunJumpForce = 6f;
    private float _wallRunCamTilt = 20;

    private bool _leftWall = false;
    private bool _rightWall = false;

    RaycastHit _leftWallHit, _rightWallHit;

    public WallRunningState(MovementController controller, StateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _transform = _controller.transform;
        _rb = _controller.rb;
        _rb.useGravity = false;
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

        if(!CanWallRun()) {
            if(_controller.IsGrounded()) {
                Debug.Log("WallRun -> Ground");
                _sm.ChangeState(new GroundState(_controller, _sm));
                return;
            }
            
            Debug.Log("WallRun -> Air");
            _sm.ChangeState(new AirState(_controller, _sm));
            return;
        }

        _rb.AddForce(Vector3.down * _wallRunGravity, ForceMode.Force);
        float tiltFactor = _leftWall ? -1 : 1;
        _controller.Tilt(tiltFactor * _wallRunCamTilt);

        if (!_jump || (!_leftWall && !_rightWall)) return;

        RaycastHit wall = _leftWall ? _leftWallHit : _rightWallHit;
        Vector3 direction = _transform.up + wall.normal;

        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        float force = CalcJumpForce();
        _rb.AddForce(direction * force, ForceMode.Force);
    }

    public override void Exit()
    {
        base.Exit();
        _rb.useGravity = true;
        _controller.Tilt(0, true);
    }

    float CalcJumpForce() {
        float hVel = Mathf.Sqrt(Mathf.Pow(_rb.velocity.x, 2) + Mathf.Pow(_rb.velocity.z, 2));
        return _wallRunJumpForce * Mathf.Clamp(hVel * 5, 50, 150);
    }

    bool CanWallRun()
    {
        _rightWall = Physics.Raycast(_transform.position, _transform.right, out _rightWallHit, _wallDist);
        _leftWall = Physics.Raycast(_transform.position, -_transform.right, out _leftWallHit, _wallDist);

        return !Physics.Raycast(_transform.position, Vector3.down, _minWallrunHeight) && (_leftWall || _rightWall);
    }
}