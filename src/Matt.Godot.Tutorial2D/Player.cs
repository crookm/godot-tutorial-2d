using Godot;

namespace Matt.Godot.Tutorial2D;

public partial class Player : Area2D
{
    [Export] public int Speed { get; set; } = 400;

    [Signal]
    public delegate void HitEventHandler();

    private Vector2 ScreenSize { get; set; }

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        Hide();
    }

    public override void _Process(double delta)
    {
        var velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_up")) velocity.Y -= 1;
        if (Input.IsActionPressed("move_down")) velocity.Y += 1;
        if (Input.IsActionPressed("move_left")) velocity.X -= 1;
        if (Input.IsActionPressed("move_right")) velocity.X += 1;

        var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            sprite.Play();

            // Y-axis animations should take priority
            // > Avoids 'walking' up or down when moving diagonally - makes more sense to me
            if (velocity.Y != 0)
            {
                sprite.Animation = "up";
                sprite.FlipV = velocity.Y > 0;
            }
            else if (velocity.X != 0)
            {
                sprite.Animation = "walk";
                sprite.FlipV = false;
                sprite.FlipH = velocity.X < 0; // sprite normally faces right, flip if moving left
            }
        }
        else sprite.Stop();

        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();

        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    private void OnBodyEntered(Node2D body)
    {
        Hide();
        EmitSignal(SignalName.Hit);

        // Disable the collision hitbox to prevent firing this event multiple times
        // > Must be deferred as we cannot make changes to physics while processing physics, Godot will wait until it is safe to do so
        // > We use `*.PropertyNames.*` because `SetDeferred` uses a StringName rather than the property itself, this avoids additional object allocations
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
}