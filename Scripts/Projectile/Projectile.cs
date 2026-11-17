using Godot;

public partial class Projectile : Hitbox
{
	[Export] private ProjectileData Data;

	private Vector2 direction;

	public override void _Ready()
	{
		Initialize(Data.Damage);

		Hit += QueueFree;

		GetTree().CreateTimer(Data.Lifetime).Timeout += QueueFree;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Data.Speed * direction * (float)delta;
		Rotation = direction.Angle();
	}

	public void SetDirection(Vector2 direction)
	{
		this.direction = direction;
	}
}
