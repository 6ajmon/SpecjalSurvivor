using Godot;

public partial class Shotgun : Node2D
{
    [Export] private PackedScene _bulletScene; // Assign your bullet scene in the Inspector
    [Export] private float _bulletSpeed = 600f; // Speed of the bullet
    [Export] private float _fireRate = 0.4f; // Time between shots (in seconds)

	private Marker2D _marker;
	private Timer _timer;
	private bool _canShoot = true;

	public override void _Ready()
	{
		_marker = GetNode<Marker2D>("AnimatedSprite2D/BulletSpawnPoint");
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimerTimeout;
		_timer.WaitTime = _fireRate;
	}

    public override void _PhysicsProcess(double delta)
    {

        // Rotate the gun to point at the mouse
        RotateGunTowardsMouse();

        // Handle shooting
        if (Input.IsActionPressed("Shoot") && _canShoot)
		{
			Shoot();
			_canShoot = false;
			_timer.Start();
		}
    }

    private void RotateGunTowardsMouse()
    {
        // Get the mouse position in global coordinates
        Vector2 mousePosition = GetGlobalMousePosition();

        // Get the direction from the gun to the mouse
        Vector2 direction = (mousePosition - GlobalPosition).Normalized();

        // Calculate the angle to rotate the gun
        float angle = direction.Angle();

        // Set the gun's rotation
        Rotation = angle;
    }

	private void OnTimerTimeout()
	{
		_canShoot = true;
	}
    private void Shoot()
    {
        if (_bulletScene == null)
        {
            GD.PrintErr("Bullet scene is not assigned!");
            return;
        }

        // Instantiate the bullet
        Node2D bullet = (Node2D)_bulletScene.Instantiate();
        GetParent().AddChild(bullet); // Add the bullet to the scene

        // Set the bullet's position and rotation to match the gun
        bullet.GlobalPosition = _marker.GlobalPosition;
        bullet.Rotation = Rotation + Mathf.Pi / 2;

        // Apply velocity to the bullet
        if (bullet is Bullet bulletScript)
        {
            bulletScript.Velocity = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)) * _bulletSpeed;
        }
    }
}