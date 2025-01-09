using Godot;
using System;

public partial class Neptun : CharacterBody2D
{
    public float Speed = 0f; // Movement speed of the enemy
    public float DetectionRange = 0f; // Range within which the enemy detects the player

    private Player _player; // Reference to the player
    private bool _isChasing = false; // Whether the enemy is currently chasing the player

    public override void _Ready()
    {
        // Try to find the player in the scene (assuming the player is named "Player")
        _player = GetNodeOrNull<Player>("/root/Level/Player");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_player == null)
		{
			GD.PrintErr("Player not found!");
            return;
		}

        // Calculate distance to the player
        float distanceToPlayer = Position.DistanceTo(_player.Position);

        // Check if the player is within detection range
        if (distanceToPlayer <= DetectionRange)
        {
            _isChasing = true;
        }
        else
        {
            _isChasing = false;
        }

        // Move towards the player if chasing
        if (_isChasing)
        {
            Vector2 direction = (_player.Position - Position).Normalized();
            Velocity = direction * Speed;
        }
        else
        {
            Velocity = Vector2.Zero;
        }

        MoveAndSlide();
    }

    public override void _Draw()
    {
        // Optional: Draw detection range for debugging
        DrawCircle(Vector2.Zero, DetectionRange, new Color(1, 0, 0, 0.2f));
    }
}
