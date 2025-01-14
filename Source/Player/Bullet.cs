using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public Vector2 Velocity { get; set; } // Velocity of the bullet

    public override void _Process(double delta)
    {
        // Move the bullet
        Position += Velocity * (float)delta;
    }
	private void DealDamage(Area2D area)
	{
		if (area is HitboxComponent)
        {
            HitboxComponent hitbox = (HitboxComponent)area;
            hitbox.Damage(50);
        }
	}
    private void LifespanTimeout()
    {
        QueueFree();
    }
}
