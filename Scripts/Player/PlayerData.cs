using Godot;

[GlobalClass]
public partial class PlayerData : Resource
{
    [Export] public int Health = 300;
    [Export] public float MoveSpeed = 300.0f;
    [Export] public float DashSpeed = 900.0f;
}
