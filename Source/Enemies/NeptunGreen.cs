using Godot;
using System;

public partial class NeptunGreen : Neptun
{
	[Export] private float _speed = 300f;
	[Export] private float _detectionRange = 300f;
	public override void _Ready()
	{
		Speed = _speed;
		DetectionRange = _detectionRange;
		base._Ready();
	}
}
