using UnityEngine;

public abstract class MovementState:State {

    protected MovementController _controller;

    public MovementState(MovementController controller, StateMachine stateMachine):base(stateMachine) {
        _controller = controller;
    }
}
