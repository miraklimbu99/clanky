using Godot;

[GlobalClass]
public partial class Movement : Node2D
{
	private CharacterBody2D character;

	public void Initialize(CharacterBody2D character)
	{
		this.character = character;
	}

	public void Move(Vector2 direction, float speed)
	{
		character.Velocity = direction * speed;
		character.MoveAndSlide();
	}
}
