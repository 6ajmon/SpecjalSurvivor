using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{

	[Export] private State initialState;
	private State currentState;
	private Dictionary<string, State> _states = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var child in GetChildren())
		{
			if (child is State state)
			{
				_states.Add(state.Name.ToString().ToLower(), state);
				(child as State).Transitioned += OnChildTransitioned;
			}
		}
		if (initialState != null)
		{
			initialState.Enter();
			currentState = initialState;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (currentState != null)
		{
			currentState.Update((float)delta);
		}
	}

    public override void _PhysicsProcess(double delta)
    {
        if (currentState != null)
		{
			currentState.PhysicsUpdate((float)delta);
		}
    }
	private void OnChildTransitioned(State state, string newStateName)
	{
		if (state != currentState)
		{
			GD.PrintErr("Transitioned from a state that is not the current state");
			return;
		}

		State newState = _states[newStateName.ToLower()];
		if (newState == null)
		{
			GD.PrintErr("State not found: " + newStateName);
			return;
		}

		if (currentState != null)
		{
			currentState.Exit();
		}
		newState.Enter();
		currentState = newState;
	}
}
