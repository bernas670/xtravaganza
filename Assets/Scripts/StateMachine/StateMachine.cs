using UnityEngine;

public class StateMachine {
    
    private State _currentState;

    public void Initialize(State startState) {
        _currentState = startState;
        _currentState.Enter();
    }

    public void ChangeState(State newState) {
        _currentState.Exit();

        _currentState = newState;
        newState.Enter();
    }
    
    public State GetState() {
        return _currentState;
    }

    public void HandleInput() {
        _currentState.HandleInput();
    }

    public void LogicUpdate() {
        _currentState.LogicUpdate();
    }

    public void PhysicsUpdate() {
        _currentState.PhysicsUpdate();
    }
}