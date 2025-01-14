using Godot;
using System;

public partial class HitboxComponent : Area2D
{
	[Export] public HealthComponent HealthComponent;
	
	public void Damage(float damage)
	{
		if (HealthComponent != null)
		{
			HealthComponent.Damage(damage);
		}
	}
}
