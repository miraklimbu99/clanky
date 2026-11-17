using Godot;
using System;

public partial class PlayerGhost : Sprite2D
{
	private Timer timer;

	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Timeout += QueueFree;
	}
}
