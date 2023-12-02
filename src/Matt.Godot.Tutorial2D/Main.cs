using Godot;

namespace Matt.Godot.Tutorial2D;

public partial class Main : Node
{
    [Export] public PackedScene? MobScene { get; set; }

    private int _score;

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    /// <summary>
    /// Setup a new game session, initiating a short grace period before things begin.
    /// </summary>
    private void NewGame()
    {
        _score = 0;

        var player = GetNode<Player>("Player");
        var startPos = GetNode<Marker2D>("StartPosition");

        player.Start(startPos.Position);
        GetNode<Timer>("StartTimer").Start();
    }

    /// <summary>
    /// End the game when hit by an enemy.
    /// </summary>
    private void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    /// <summary>
    /// Each time the score timer elapses (each second), we increment the score.
    /// </summary>
    private void OnScoreTimerTimeout() => _score++;

    /// <summary>
    /// After a few seconds grace period, the game begins.
    /// </summary>
    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    /// <summary>
    /// Spawn a new mob at a random point along the spawn path, pointing a random direction, with a random velocity.
    /// </summary>
    private void OnMobTimerTimeout()
    {
        if (MobScene is null) return;

        var mob = MobScene.Instantiate<Mob>();

        // Get a random spawn location along the spawn path
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        // Set the mob's properties
        var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);

        mob.Position = mobSpawnLocation.Position;
        mob.Rotation = direction;

        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        AddChild(mob);
    }
}