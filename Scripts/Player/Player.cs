using Godot;

public partial class Player : CharacterBody2D
{
	[ExportGroup("Data")]
	[Export] private PlayerData data;

	[ExportGroup("Components")]
	[Export] private Movement movement;
	[Export] private Health health;
	[Export] private Hurtbox hurtbox;
	[Export] private Dash dash;
	[Export] private WeaponManager weaponManager;
	[Export] private Sprite sprite;
	[Export] private Audio audio;
	[Export] private Particle particle;

	[ExportGroup("Audio Sources")]
	[Export] private AudioStreamPlayer2D dashAudio;
	[Export] private AudioStreamPlayer2D hurtAudio;

	[ExportGroup("HUD")]
	[Export] private HUD hud;

	private Camera camera;
	private CpuParticles2D hurtParticles;

	public override void _Ready()
	{
		movement.Initialize(this);

		health.Initialize(data.Health);
		health.HealthChanged += hud.UpdateHealthLabel;

		hurtbox.Hit += damage =>
		{
			health.TakeDamage(damage);

			hud.ShowHurtFlash();

			audio.Play(hurtAudio);

			camera.Shake(10.0f, 0.2f);

			particle.Spawn(hurtParticles);
		};

		dash.Initialize(0.3f);

		sprite.Initialize(GetNode<Sprite2D>("Sprite"));
		hud.UpdateHealthLabel(data.Health);

		camera = GetNode<Camera>("Camera2D");

		hurtParticles = GetNode<CpuParticles2D>("HurtParticles");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (Input.IsActionJustPressed("dash"))
		{
			if (dash.StartDash())
				dashAudio.Play();
		}

		if (dash.isDashing)
			hurtbox.SetCollisionLayerValue(3, false);
		else
			hurtbox.SetCollisionLayerValue(3, true);

		float speed = dash.isDashing ? data.DashSpeed : data.MoveSpeed;
		movement.Move(direction, speed);
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("switch_weapon"))
			weaponManager.SwitchWeapon();

		Vector2 direction = (GetGlobalMousePosition() - Position).Normalized();
		weaponManager.currentWeapon.GlobalPosition = Position + direction * 15.0f;
		weaponManager.currentWeapon.LookAt(GetGlobalMousePosition());
	}
}
