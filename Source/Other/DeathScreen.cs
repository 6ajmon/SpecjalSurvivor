using Godot;

public partial class DeathScreen : CanvasLayer
{
    public override void _Ready()
    {
        // Connect the button's pressed signal to the restart method
        GetNode<Button>("Button").Pressed += OnRestartButtonPressed;
        GetTree().Paused = true;

    }

    private void OnRestartButtonPressed()
    {
		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
        var killCounter = GetTree().CurrentScene.GetNode<KillCounter>("/root/Level/CanvasLayer/KillCounter");
        gameEvents.EnemyKilled -= killCounter.IncrementKillCount;

        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}