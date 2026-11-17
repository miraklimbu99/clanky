using Godot;

public partial class Shooter : Enemy
{
	private Weapon weapon;

	public override void _Ready()
	{
		base._Ready();

		weapon = GetNode<Weapon>("EnemyPistol");
	}

	public override void _Process(double delta)
	{
		Vector2 direction = (player.Position - Position).Normalized();
		weapon.GlobalPosition = Position + direction * 15.0f;
		weapon.LookAt(player.Position);
	}
}
