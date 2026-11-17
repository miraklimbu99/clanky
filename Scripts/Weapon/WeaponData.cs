using Godot;

[GlobalClass]
public partial class WeaponData : Resource
{
    [Export] public float FireRate = 0.5f;
    [Export] public int PelletCount = 1;
    [Export] public float SpreadDegrees = 10.0f;
    [Export] public PackedScene ProjectileScene;
}
