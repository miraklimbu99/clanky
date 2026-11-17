using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
	[ExportGroup("Data")]
	[Export] protected EnemyData data;

	[ExportGroup("Components")]
	[Export] protected Movement movement;
	[Export] private Health health;
	[Export] private Hurtbox hurtbox;
	[Export] private Hitbox hitbox;
	[Export] private Sprite sprite;
	[Export] private Particle particle;
	[Export] private Audio audio;

	public event Action Died;
	protected Player player;
	private CpuParticles2D hurtParticles;
	private AudioStreamPlayer2D hurtAudio;

	public override void _Ready()
	{
		movement.Initialize(this);

		health.Initialize(data.Health);
		health.Died += () =>
		{
			Died?.Invoke();

			QueueFree();
		};

		hurtbox.Hit += damage =>
		{
			health.TakeDamage(damage);

			particle.Spawn(hurtParticles);

			audio.Play(hurtAudio);
		};

		hitbox.Initialize(data.Damage);

		sprite.Initialize(GetNode<Sprite2D>("Sprite"));

		player = GetTree().GetFirstNodeInGroup("Player") as Player;

		hurtParticles = GetNode<CpuParticles2D>("HurtParticles");

		hurtAudio = GetNode<AudioStreamPlayer2D>("HurtAudio");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = (player.Position - Position).Normalized();
		movement.Move(direction, data.MoveSpeed);
	}
}
