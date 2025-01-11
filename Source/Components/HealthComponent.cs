using Godot;
using System;

public partial class HealthComponent : Node2D
{

	[Export] public float MaxHealth = 100;
	public float CurrentHealth;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
	}
}
