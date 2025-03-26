using Godot;
using System;

public partial class NeptunDashState : State
{
	[Export] private Neptun neptun;
    [Export] private float DashSpeed = 1000f;
	[Export] private float ChargeTime = 0.5f;
	[Export] private float DashTime = 0.5f;
	private Vector2 direction;
	private Player player;
	private float dashSpeed = 1000f;
	private float chargeTime = 0.5f;
	private float dashTime = 0.5f;

	public override void Enter()
	{
		base.Enter();

		dashSpeed = DashSpeed;
		chargeTime = ChargeTime;
		dashTime = DashTime;

		player = GetTree().GetFirstNodeInGroup("player") as Player;
		direction = player.GlobalPosition - neptun.GlobalPosition;
		direction = direction.Normalized();
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

		if (chargeTime > 0)
		{
			chargeTime -= delta;
		}
		else
		{
			if (dashTime > 0)
			{
				dashTime -= delta;
				neptun.Position += direction * dashSpeed * delta;
			}
			else
			{
				EmitSignal(nameof(Transitioned), this, "follow");
			}

		}

	}
}
