using Godot;


[GlobalClass]
public partial class Particle : Node2D
{
	public void Spawn(CpuParticles2D particleTemplate)
	{
		CpuParticles2D particle = (CpuParticles2D)particleTemplate.Duplicate();

		particle.Position = particleTemplate.GlobalPosition;

		particle.Emitting = true;

		particle.Finished += particle.QueueFree;

		GetTree().CurrentScene.AddChild(particle);
	}
}
