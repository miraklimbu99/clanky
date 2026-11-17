using Godot;

[GlobalClass]
public partial class Sprite : Node2D
{
	private Sprite2D sprite;

	public void Initialize(Sprite2D sprite)
	{
		this.sprite = sprite;

		sprite.FlipH = GD.RandRange(0, 1) == 0;
		float direction = GD.RandRange(0, 1) == 0 ? -1 : 1;

		Tween tween = CreateTween();
		tween.SetLoops();

		tween.TweenProperty(sprite, "rotation", Mathf.DegToRad(-20.0f * direction), 0.5f);
		tween.TweenProperty(sprite, "rotation", Mathf.DegToRad(20.0f * direction), 0.5f);
	}
}
