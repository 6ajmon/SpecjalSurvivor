using Godot;
using System;

public partial class Bullet : Node2D
{
	[Export] public Vector2 Velocity { get; set; } // Velocity of the bullet

    public override void _Process(double delta)
    {
        // Move the bullet
        Position += Velocity * (float)delta;
    }
}
