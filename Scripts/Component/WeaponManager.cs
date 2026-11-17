using System;
using Godot;

[GlobalClass]
public partial class WeaponManager : Node2D
{
	[Export] private PackedScene pistolScene;
	[Export] private PackedScene shotgunScene;

	private Weapon[] weapons = new Weapon[2];

	public Weapon currentWeapon;

	public override void _Process(double delta)
	{
		Vector2 direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		currentWeapon.GlobalPosition = GlobalPosition + direction * 15.0f;
		currentWeapon.LookAt(GetGlobalMousePosition());
	}

	public void AddWeapon(WeaponType type)
	{
		currentWeapon?.UnEquip();

		Weapon weapon;

		switch (type)
		{
			case WeaponType.Pistol:
				weapon = pistolScene.Instantiate<Weapon>();
				break;
			case WeaponType.Shotgun:
				weapon = shotgunScene.Instantiate<Weapon>();
				break;
			default:
				return;
		}

		if (weapons[0] == null)
			weapons[0] = weapon;
		else if (weapons[1] == null)
			weapons[1] = weapon;

		currentWeapon = weapon;

		currentWeapon.Equip();

		GetParent().GetParent().AddChild(currentWeapon);
	}

	public bool IsFull()
	{
		foreach (var slot in weapons)
		{
			if (slot == null)
				return false;
		}

		return true;
	}

	public void SwitchWeapon()
	{
		if (IsFull())
		{
			currentWeapon.UnEquip();

			int index = Array.IndexOf(weapons, currentWeapon);

			currentWeapon = weapons[1 - index];
			currentWeapon.Equip();
		}
	}
}
