using System;
using Godot;

[GlobalClass]
public partial class Health : Node2D
{
	public int health;

	public event Action<int> HealthChanged;
	public event Action Died;

	public void Initialize(int health)
	{
		this.health = health;
	}

	public void TakeDamage(int damage)
	{
		if (health <= 0)
			return;

		health -= damage;
		HealthChanged?.Invoke(health);

		if (health <= 0)
			Died?.Invoke();
	}
}
