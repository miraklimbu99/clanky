using Godot;
using System;

public partial class WaveManager : Node
{
	[ExportGroup("Enemy Scenes")]
	[Export] private PackedScene rusherScene;
	[Export] private PackedScene tankerScene;
	[Export] private PackedScene shooterScene;

	[ExportGroup("Spawners")]
	[Export] private Node2D spawnerOne;
	[Export] private Node2D spawnerTwo;
	[Export] private Node2D spawnerThree;
	[Export] private Node2D spawnerFour;
	[Export] private int wave = 0;

	[ExportGroup("HUD")]
	[Export] private HUD hud;

	private int enemiesAlive = 0;
	private int rushersAlive = 0;
	private int tankersAlive = 0;
	private int shootersAlive = 0;

	private enum Phase
	{
		Wave,
		Intermission
	}
	private Phase currentPhase = Phase.Intermission;

	private Timer intermissionTimer;

	public override void _Ready()
	{
		intermissionTimer = GetNode<Timer>("IntermissionTimer");

		intermissionTimer.Start();

		intermissionTimer.Timeout += StartWave;
	}

	public override void _Process(double delta)
	{
		if (currentPhase == Phase.Intermission)
		{
			hud.UpdateIntermissionLabel((float)Math.Round(intermissionTimer.TimeLeft, 1));
		}
		else if (currentPhase == Phase.Wave && enemiesAlive <= 0)
		{
			StartIntermission();
		}
	}

	private void StartIntermission()
	{
		currentPhase = Phase.Intermission;

		intermissionTimer.Start();

		hud.UpdateStatusLabel("Intermission");
	}

	private void StartWave()
	{
		currentPhase = Phase.Wave;

		wave++;

		hud.UpdateStatusLabel("Wave Started!!!");
		hud.UpdateWaveLabel(wave);

		GenerateWave();

		SpawnWave();
	}

	private void GenerateWave()
	{
		rushersAlive = wave + 2;
		tankersAlive = wave + 1;
		shootersAlive = wave / 2;

		enemiesAlive = rushersAlive + tankersAlive + shootersAlive;
	}

	private void SpawnWave()
	{
		for (int i = 0; i < rushersAlive; i++)
		{
			SpawnEnemy(rusherScene);
		}

		for (int i = 0; i < tankersAlive; i++)
		{
			SpawnEnemy(tankerScene);
		}

		for (int i = 0; i < shootersAlive; i++)
		{
			SpawnEnemy(shooterScene);
		}
	}

	private void SpawnEnemy(PackedScene enemyScene)
	{
		Enemy enemy = enemyScene.Instantiate<Enemy>();

		enemy.Died += OnEnemyDeath;

		Vector2[] positions =
		{
			spawnerOne.Position,
			spawnerTwo.Position,
			spawnerThree.Position,
			spawnerFour.Position
		};
		Vector2 position = positions[GD.RandRange(0, 3)];

		enemy.Position = new Vector2(
			(float)GD.RandRange(position.X - 300.0, position.X + 300.0),
			(float)GD.RandRange(position.Y - 300.0, position.Y + 300.0)
		);

		GetTree().CurrentScene.AddChild(enemy);

		hud.UpdateEnemiesAliveLabel(enemiesAlive);
	}

	private void OnEnemyDeath()
	{
		enemiesAlive--;

		hud.UpdateEnemiesAliveLabel(enemiesAlive);
	}
}
