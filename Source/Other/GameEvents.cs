using Godot;
using System;

public partial class GameEvents : Node
{
	[Signal]
    public delegate void EnemyKilledEventHandler();
	
	public void PlayerDied(){
		var deathScreen = ResourceLoader.Load<PackedScene>("res://Source/Other/DeathScreen.tscn").Instantiate();
        GetTree().CurrentScene.AddChild(deathScreen);
	}
}
