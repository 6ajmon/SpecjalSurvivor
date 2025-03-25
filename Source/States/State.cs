using Godot;

public partial class State : Node
{

    // sygnał wysyłany do maszyny stanów, aby zmienić stan
    [Signal] 
    public delegate void TransitionedEventHandler(State state, string newStateName);

    // wywoływane gdy stan zaczyna się
    public virtual void Enter()
    {
    }

    // wywoływane gdy stan kończy się
    public virtual void Exit()
    {
    }

    // wywoływane co klatkę dopóki stan jest aktywny
    public virtual void Update(float delta)
    {
    }

    // wywoływane co klatkę dopóki stan jest aktywny
    public virtual void PhysicsUpdate(float delta)
    {
    }
}
