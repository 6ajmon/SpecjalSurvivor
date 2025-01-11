using Godot;
using System;

public partial class HealthComponent : Node2D
{

	[Export] public float MaxHealth = 100;
	public float CurrentHealth;
	private ProgressBar _healthBar;
	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
		_healthBar = GetChild<ProgressBar>(0);
		_healthBar.MaxValue = MaxHealth;
		_healthBar.Value = CurrentHealth;
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
		if (_healthBar != null)
		{
			_healthBar.Value = CurrentHealth;
		}
		if (CurrentHealth <= 0)
		{
			GetParent().QueueFree();
		}
	}
}
