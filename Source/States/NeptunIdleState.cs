using Godot;

public partial class NeptunIdleState : State
{


    [Export] Neptun neptun;
    [Export] float moveSpeed = 100f;
    Vector2 moveDirection = Vector2.Zero;
    float wanderTime = 0f;
    float waitTime = 0f;
    Timer timer;
    Player player;

    private void OnTimerTimeout()
    {
        randomizeWander();
    }

    private void randomizeWander(){
        moveDirection = new Vector2((float)GD.RandRange(-1, 1), (float)GD.RandRange(-1, 1)).Normalized();
        wanderTime = (float)GD.RandRange(1, 3);
        waitTime = (float)GD.RandRange(1, 3);
    }

    public override void Enter()
    {
        base.Enter();

        timer = new Timer();
        AddChild(timer);
        randomizeWander();
        player = GetTree().GetFirstNodeInGroup("player") as Player;

        timer.Timeout += OnTimerTimeout;
        timer.WaitTime = waitTime;
        timer.OneShot = false;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update(float delta)
    {
        base.Update(delta);
        
        if (wanderTime > 0)
        {
            wanderTime -= delta;
            if (wanderTime <= 0)
            {
                timer.Start();
            }
        }
        else
        {
            timer.Start();
        }
    }
    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);

        if (neptun == null)
        {
            GD.PrintErr("Neptun not found!");
            return;
        }
        neptun.Position += moveDirection * moveSpeed * delta;

        if (player != null)
        {
            Vector2 direction = player.GlobalPosition - neptun.GlobalPosition;
            if (direction.Length() < 200)
            {
                EmitSignal(nameof(Transitioned), this, "follow");
            }
        }
    }
}