using Godot;
using System;

public partial class EnemySpawner : Node2D
{
    [Export] private PackedScene _enemyScene; // Assign your enemy scene in the Inspector
    [Export] private int _enemiesPerWave = 5; // Number of enemies per wave
    [Export] private float _timeBetweenWaves = 10f; // Delay between waves (in seconds)
    [Export] private float _timeBetweenSpawns = 1f; // Delay between spawning individual enemies (in seconds)
    [Export] private int _maxWaves = 5; // Maximum number of waves to spawn
    [Export] private float _spawnRadius = 200f; // Radius around the player to spawn enemies

    private int _currentWave = 0;
    private int _enemiesRemainingInWave;
    private float _waveTimer = 0f;
    private float _spawnTimer = 0f;
    private bool _isSpawning = false;
    private Player _player;

    public override void _Ready()
    {
        // Find the player node (assuming it's named "Player")
        _player = GetNodeOrNull<Player>("/root/Level/Player");

        if (_player == null)
        {
            GD.PrintErr("Player not found!");
            return;
        }

        StartNextWave();
    }

    public override void _Process(double delta)
    {
        if (_currentWave >= _maxWaves)
        {
            GD.Print("All waves completed!");
            return;
        }

        if (_isSpawning)
        {
            _spawnTimer += (float)delta;

            // Spawn enemies with a delay between them
            if (_spawnTimer >= _timeBetweenSpawns && _enemiesRemainingInWave > 0)
            {
                SpawnEnemy();
                _enemiesRemainingInWave--;
                _spawnTimer = 0f;
            }

            // Check if the current wave is complete
            if (_enemiesRemainingInWave <= 0)
            {
                _isSpawning = false;
                _waveTimer = 0f;
            }
        }
        else
        {
            _waveTimer += (float)delta;

            // Start the next wave after the delay
            if (_waveTimer >= _timeBetweenWaves)
            {
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {
        _currentWave++;
        _enemiesRemainingInWave = _enemiesPerWave;
        _isSpawning = true;
        GD.Print($"Starting Wave {_currentWave}");
    }

    private void SpawnEnemy()
    {
        if (_enemyScene == null)
        {
            GD.PrintErr("Enemy scene is not assigned!");
            return;
        }

        if (_player == null)
        {
            GD.PrintErr("Player not found!");
            return;
        }

        // Instantiate the enemy
        Node2D enemy = (Node2D)_enemyScene.Instantiate();
        AddChild(enemy);

        // Calculate a random position within the spawn radius around the player
        Vector2 spawnPosition = GetRandomPositionAroundPlayer();
        enemy.Position = spawnPosition;

        GD.Print($"Spawned enemy at {spawnPosition}");
    }

    private Vector2 GetRandomPositionAroundPlayer()
    {
        // Define a minimum distance from the player
        float minDistance = 200f; // Adjust this value as needed

        // Get a random angle in radians
        float angle = (float)GD.RandRange(0, 2 * Mathf.Pi);

        // Get a random distance within the spawn radius, ensuring it's not too close to the player
        float distance = (float)GD.RandRange(minDistance, _spawnRadius);

        // Calculate the spawn position relative to the player
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return _player.Position + spawnOffset;
    }
}