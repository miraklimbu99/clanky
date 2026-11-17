using Godot;

public partial class Weapon : Node2D
{
	[ExportGroup("Data")]
	[Export] private WeaponData Data;

	[ExportGroup("Components")]
	[Export] private Audio audio;
	[Export] private Particle particle;

	private AudioStreamPlayer2D shootAudio;
	private Timer fireRateTimer;
	private Marker2D muzzle;
	protected CpuParticles2D shootParticles;

	public override void _Ready()
	{
		shootAudio = GetNode<AudioStreamPlayer2D>("ShootAudio");

		fireRateTimer = GetNode<Timer>("FireRateTimer");
		fireRateTimer.WaitTime = Data.FireRate;
		fireRateTimer.Start();
		fireRateTimer.Timeout += OnFireRateTimerTimeout;

		muzzle = GetNode<Marker2D>("Muzzle");
		shootParticles = GetNode<CpuParticles2D>("ShootParticles");
	}

	private void OnFireRateTimerTimeout()
	{
		Shoot();
	}

	private void Shoot()
	{
		SpawnProjectiles();
		audio.Play(shootAudio);
		particle.Spawn(shootParticles);
	}

	public override void _Process(double delta)
	{
		float rotationDegrees = Mathf.Wrap(RotationDegrees, 0, 360);

		if (rotationDegrees > 90 && rotationDegrees < 270)
			Scale = new Vector2(1, -1);
		else
			Scale = new Vector2(1, 1);
	}

	private void SpawnProjectiles()
	{
		Vector2 direction = Vector2.Right.Rotated(GlobalRotation);

		for (int i = 0; i < Data.PelletCount; i++)
		{
			float spread = Mathf.DegToRad((float)GD.RandRange(-Data.SpreadDegrees, Data.SpreadDegrees));
			SpawnProjectile(direction.Rotated(spread));
		}
	}

	private void SpawnProjectile(Vector2 direction)
	{
		Projectile projectile = Data.ProjectileScene.Instantiate<Projectile>();

		projectile.SetDirection(direction);
		projectile.Position = muzzle.GlobalPosition;

		GetTree().CurrentScene.AddChild(projectile);
	}

	public void Equip()
	{
		Visible = true;
		ProcessMode = ProcessModeEnum.Inherit;
	}

	public void UnEquip()
	{
		Visible = false;
		ProcessMode = ProcessModeEnum.Disabled;
	}
}
