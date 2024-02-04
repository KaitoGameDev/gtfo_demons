using Godot;

namespace gtfo_demons.Gameplay.Presentation;

public partial class WaveTimer : Control
{
	[Signal]
	public delegate void OnWaveSurvivedEventHandler();
	
	private Timer _waveTime;
	private ProgressBar _progressBar;

	public override void _Ready() {
		_progressBar = GetNode<ProgressBar>("ProgressBar");
		_waveTime = GetNode<Timer>("Timer");

		_waveTime.Connect(Timer.SignalName.Timeout, Callable.From(OnTimeout));
	}

	private void OnTimeout() {
		EmitSignal(SignalName.OnWaveSurvived);
	}

	public override void _Process(double delta) {
		_progressBar.Value = _waveTime.TimeLeft;
	}
}