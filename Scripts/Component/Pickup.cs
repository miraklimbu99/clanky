using Godot;

[GlobalClass]
public partial class Pickup : Area2D
{
	[Export] private WeaponType type;

	public override void _Ready()
	{
		BodyEntered += body =>
		{
			if (body is Player player)
			{
				if (!player.WeaponManager.IsFull())
				{
					player.WeaponManager.AddWeapon(type);

					GetParent().QueueFree();
				}
			}
		};
	}
}
