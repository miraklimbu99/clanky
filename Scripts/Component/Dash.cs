using Godot;

[GlobalClass]
public partial class Dash : Node2D
{
	private Timer dashTimer;

	public bool isDashing;

	public void Initialize(float duration)
	{
		dashTimer = new Timer();
		dashTimer.OneShot = true;
		dashTimer.WaitTime = duration;

		AddChild(dashTimer);

		dashTimer.Timeout += () => isDashing = false;
	}

	public bool StartDash()
	{
		if (isDashing)
			return false;
		
		isDashing = true;
		dashTimer.Start();

		return true;
	}
}
