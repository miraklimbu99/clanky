using Godot;

[GlobalClass]
public partial class Audio : Node2D
{
	public void Play(AudioStreamPlayer2D _audio)
	{
		AudioStreamPlayer2D audio = (AudioStreamPlayer2D)_audio.Duplicate();

		audio.PitchScale = (float)GD.RandRange(0.7, 1.3);
		audio.Position = _audio.GlobalPosition;

		audio.Finished += audio.QueueFree;

		GetTree().CurrentScene.AddChild(audio);

		audio.Play();
	}
}
