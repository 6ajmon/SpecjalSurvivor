using Godot;
using System;

public partial class NeptunFollowState : State
{
	[Export] private Neptun _neptun;
	private Player _player;
	private Vector2 _direction;
	[Export] private float moveSpeed = 160f;
	[Export] private float detectionRange = 300f;
	[Export] private float ChasingTime = 3f;
	private float chasingTime = 3f;

	public override void Enter()
	{
		base.Enter();
		_player = GetTree().GetFirstNodeInGroup("player") as Player;

		chasingTime = ChasingTime;

	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update(float delta)
	{
		base.Update(delta);
	}

	public override void PhysicsUpdate(float delta)
	{
		base.PhysicsUpdate(delta);

		Vector2 direction = _player.GlobalPosition - _neptun.GlobalPosition;
		if (direction.Length() > 15)
		{
			_neptun.Position += direction.Normalized() * moveSpeed * delta;
		}
		else
		{
			_neptun.Position = _neptun.Position;
		}
		if (direction.Length() > detectionRange)
		{
			EmitSignal(nameof(Transitioned), this, "idle");
		}
		if (chasingTime > 0)
		{
			chasingTime -= delta;
		}
		else
		{
			EmitSignal(nameof(Transitioned), this, "dash");
		}
	}

}
