using Godot;

public partial class Rusher : Enemy
{
	[Export] private Dash dash;

	private Timer dashCooldown;

	private float dashSpeed = 700.0f;
	private Vector2 dashDirection;

	public override void _Ready()
	{
		base._Ready();

		dash.Initialize(0.5f);

		dashCooldown = GetNode<Timer>("DashCooldown");
		dashCooldown.Start();
		dashCooldown.WaitTime = GD.RandRange(3, 7);
		dashCooldown.Timeout += () =>
		{
			Vector2 direction = (player.Position - Position).Normalized();
			float spread = Mathf.DegToRad((float)GD.RandRange(-30.0f, 30.0f));
			dashDirection = direction.Rotated(spread);

			dash.StartDash();
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction;

		if (dash.isDashing)
		{
			direction = dashDirection;
		}
		else
		{
			direction = (player.Position - Position).Normalized();
		}

		float speed = dash.isDashing ? dashSpeed : data.MoveSpeed;

		movement.Move(direction, speed);
	}
}
