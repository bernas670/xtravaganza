using UnityEngine;

public abstract class State {

    protected StateMachine _sm;

    public State(StateMachine stateMachine) {
        _sm = stateMachine;
    }

    public virtual void Enter() {

    }

    public virtual void HandleInput() {

    }

    public virtual void LogicUpdate() {

    }

    public virtual void PhysicsUpdate() {
        
    }

    public virtual void Exit() {

    }
}