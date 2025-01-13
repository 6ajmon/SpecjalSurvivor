using Godot;
using System;

public partial class Neptun : Area2D
{
    public float Speed = 0f;
    public float DetectionRange = 0f;

    private Player _player;
    private bool _isChasing = false;

    public override void _Ready()
    {
        _player = GetNodeOrNull<Player>("/root/Level/Player");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_player == null)
		{
			GD.PrintErr("Player not found!");
            return;
		}

        float distanceToPlayer = Position.DistanceTo(_player.Position);

        if (distanceToPlayer <= DetectionRange)
        {
            _isChasing = true;
        }
        else
        {
            _isChasing = false;
        }

        if (_isChasing)
        {
            Vector2 direction = (_player.Position - Position).Normalized();
            Position += direction * Speed * (float)delta;
        }
    }

    public override void _Draw()
    {
        //DrawCircle(Vector2.Zero, DetectionRange, new Color(1, 0, 0, 0.2f));
    }

    // damage kontaktowy
    public virtual void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent)
        {
            HitboxComponent hitbox = (HitboxComponent)area;
            hitbox.Damage(10);
        }
    }
}
