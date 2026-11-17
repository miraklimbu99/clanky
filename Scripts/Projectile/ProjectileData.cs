using Godot;

[GlobalClass]
public partial class ProjectileData : Resource
{
	[Export] public float Speed;
	[Export] public int Damage;
	[Export] public float Lifetime;
}
