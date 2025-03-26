using Godot;
using System;

public partial class Neptun : Area2D
{
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
