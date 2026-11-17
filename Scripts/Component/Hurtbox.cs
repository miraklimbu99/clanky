using System;
using Godot;

[GlobalClass]
public partial class Hurtbox : Area2D
{
	public event Action<int> Hit;

	public void TakeHit(int damage)
	{
		Hit?.Invoke(damage);
	}
}
