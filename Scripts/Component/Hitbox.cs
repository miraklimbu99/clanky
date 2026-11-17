using System;
using Godot;

public partial class Hitbox : Area2D
{
	private int damage;

	public event Action Hit;

	public void Initialize(int damage)
	{
		this.damage = damage;

		AreaEntered += area =>
		{
			if (area is Hurtbox hurtbox)
			{
				hurtbox.TakeHit(damage);

				Hit?.Invoke();
			}
		};
	}
}
