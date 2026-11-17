using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private Label healthLabel;
	private Label weaponLabel;
	private Label intermissionLabel;
	private Label statusLabel;
	private Label enemiesAliveLabel;
	private Label waveLabel;
	private ColorRect hurtFlash;

	private Tween hurtTween;

	public override void _Ready()
	{
		healthLabel = GetNode<Label>("Control/HealthLabel");
		weaponLabel = GetNode<Label>("Control/WeaponLabel");
		intermissionLabel = GetNode<Label>("Control/IntermissionLabel");
		statusLabel = GetNode<Label>("Control/StatusLabel");
		enemiesAliveLabel = GetNode<Label>("Control/EnemiesAliveLabel");
		waveLabel = GetNode<Label>("Control/WaveLabel");
		hurtFlash = GetNode<ColorRect>("HurtFlash");
	}

	public void UpdateHealthLabel(int health)
	{
		healthLabel.Text = $"Health: {health}";
	}

	public void UpdateWeaponLabel(string weapon)
	{
		weaponLabel.Text = $"Weapon: {weapon}";
	}

	public void UpdateIntermissionLabel(float timeLeft)
	{
		intermissionLabel.Text = $"Wave starts in: {timeLeft}";
	}

	public void UpdateStatusLabel(string status)
	{
		statusLabel.Text = status;
	}

	public void UpdateEnemiesAliveLabel(int enemiesAlive)
	{
		enemiesAliveLabel.Text = $"Enemies Alive: {enemiesAlive}";
	}

	public void UpdateWaveLabel(int wave)
	{
		waveLabel.Text = $"Wave #{wave}";
	}

	public void ShowHurtFlash()
	{
		hurtTween?.Kill();

		hurtFlash.Color = new Color(1, 0, 0, 0.4f);

		hurtTween = CreateTween();

		hurtTween.TweenProperty(
			hurtFlash,
			"color:a",
			0.0f,
			0.4f
		);
	}
}
