using Godot;

[GlobalClass]
public partial class WeaponManager : Node2D
{
	[Export] private PackedScene pistolScene;
	[Export] private PackedScene shotgunScene;

	public Weapon currentWeapon;

	private bool usingPistol = true;

	public override void _Ready()
	{
		EquipWeapon(pistolScene);
	}

	private void EquipWeapon(PackedScene weaponScene)
	{
		currentWeapon?.QueueFree();

		currentWeapon = weaponScene.Instantiate<Weapon>();

		GetParent().GetParent().CallDeferred(Node.MethodName.AddChild, currentWeapon);
	}

	public void SwitchWeapon()
	{
		usingPistol = !usingPistol;

		EquipWeapon(usingPistol ? pistolScene : shotgunScene);
	}
}
