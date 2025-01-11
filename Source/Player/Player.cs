using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 200f; // Normal movement speed
    [Export] public float DashSpeed = 500f; // Speed during a dash
    [Export] public float DashDuration = 0.2f; // Duration of the dash in seconds
    [Export] public float DashCooldown = 1.0f; // Cooldown between dashes in seconds

    private Vector2 _velocity = Vector2.Zero;
    private bool _isDashing = false;
    private float _dashTime = 0f;
    private float _dashCooldownTimer = 0f;

    public override void _PhysicsProcess(double delta)
    {
        // Handle dash cooldown
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= (float)delta;
        }

        // Normal movement input
        if (!_isDashing)
        {
            Vector2 inputDirection = Input.GetVector("Move Left", "Move Right", "Move Up", "Move Down");
            _velocity = inputDirection * Speed;

            // Start dash
            if (Input.IsActionJustPressed("Dash") && _dashCooldownTimer <= 0 && inputDirection != Vector2.Zero)
            {
                _isDashing = true;
                _dashTime = DashDuration;
                _dashCooldownTimer = DashCooldown;
                _velocity = inputDirection.Normalized() * DashSpeed;
            }
        }
        else
        {
            // Dash logic
            _dashTime -= (float)delta;
            if (_dashTime <= 0)
            {
                _isDashing = false;
            }
        }

        // Apply movement
        Velocity = _velocity;
        MoveAndSlide();
    }
}
