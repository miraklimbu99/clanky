using Godot;

[GlobalClass]
public partial class EnemyData : Resource
{
	[Export] public int Health;
	[Export] public float MoveSpeed;
	[Export] public int Damage;
}
