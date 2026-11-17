using Godot;
using Microsoft.VisualBasic;

public partial class Camera : Camera2D
{
	private float shakeStrength;
	private float shakeDuration;
	private float shakeTimer;

	public override void _Process(double delta)
	{
		if (shakeTimer > 0)
		{
			shakeTimer -= (float)delta;

			float intensity = shakeStrength * (shakeTimer / shakeDuration);

			Offset = new Vector2(
				(float)GD.RandRange(-intensity, intensity),
				(float)GD.RandRange(-intensity, intensity)
			);
		}
		else
		{
			Offset = Vector2.Zero;
		}
	}

	public void Shake(float strength, float duration)
	{
		shakeStrength = strength;
		shakeDuration = duration;
		shakeTimer = duration;
	}
}
