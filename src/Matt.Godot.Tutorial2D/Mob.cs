using Godot;

namespace Matt.Godot.Tutorial2D;

public partial class Mob : RigidBody2D
{
	public override void _Ready()
	{
		var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var mobTypes = sprite.SpriteFrames.GetAnimationNames();

		sprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	public override void _Process(double delta)
	{
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		// Deletes the node and its children at the end of the frame, after all deferred updates
		QueueFree();
	}
}
