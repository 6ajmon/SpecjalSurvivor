using Godot;

public partial class State : Node
{

    [Signal] 
    public delegate void TransitionedEventHandler(State state, string newStateName);

    public virtual void Enter()
    {
    }
    public virtual void Exit()
    {
    }
    public virtual void Update(float delta)
    {
    }
    public virtual void PhysicsUpdate(float delta)
    {
    }
}
