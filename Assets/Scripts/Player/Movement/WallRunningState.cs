using UnityEngine;

public class WallRunningState : MovementState
{
    private Transform _transform;
    private Rigidbody _rb;
    private bool _wishJump = false;

    private float _wallRunGravity = -Physics.gravity.y * 0.3f;
    private float _wallRunJumpForce = 6f;
    private float _wallRunCamTilt = 20;

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
        if (Input.GetButtonDown("Jump"))
            _wishJump = true;
        if (Input.GetButtonUp("Jump"))
            _wishJump = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(!_controller.CanWallRun()) {
            if(_controller.IsGrounded()) {
                // Debug.Log("WallRun -> Ground");
                _sm.ChangeState(new GroundState(_controller, _sm));
                return;
            }
            
            // Debug.Log("WallRun -> Air");
            _sm.ChangeState(new AirState(_controller, _sm));
            return;
        }

        RaycastHit wall;
        _rb.AddForce(Vector3.down * _wallRunGravity, ForceMode.Force);
        float wallRunFactor = _controller.GetWallRunFactor(out wall);
        _controller.Tilt(wallRunFactor * _wallRunCamTilt);

        if (!_wishJump) return;

        Debug.Log("JUMPING");
        _wishJump = false;

        Vector3 direction = _transform.up + wall.normal;

        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        float force = CalcJumpForce();
        _rb.AddForce(direction * force, ForceMode.Force);
    }

    float CalcJumpForce() {
        float hVel = Mathf.Sqrt(Mathf.Pow(_rb.velocity.x, 2) + Mathf.Pow(_rb.velocity.z, 2));
        return _wallRunJumpForce * Mathf.Clamp(hVel * 5, 50, 150);
    }

    public override void Exit()
    {
        base.Exit();
        _rb.useGravity = true;
        _controller.Tilt(0, true);
    }
}