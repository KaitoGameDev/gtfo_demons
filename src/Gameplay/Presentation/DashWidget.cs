using System.Linq;
using Godot;
using gtfo_demons.InputSystem;

namespace gtfo_demons.Gameplay.Presentation;

public partial class DashWidget : Control
{
	[Signal] public delegate void OnDashStartedEventHandler();
	
	private Panel _dashContainer;
	private Timer _timer;
	private Label _buttonKey;

	private bool _isDashAllowed = true;
	private IGameplayManager _gameplayManager;
	private IInputSystem _inputSystem = new InputSystem.InputSystem();
	public override void _Ready()
	{
		_gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
		
		_buttonKey = GetNode<Label>("Panel/Key/ButtonKey");
		_timer = GetNode<Timer>("Timer");
		_timer.Connect("timeout", Callable.From(OnTimeoutRestoreDash));
		_dashContainer = GetNode<Panel>("Panel");
	}

	private void CheckUserController()
	{
		var inputs = Input.GetConnectedJoypads();
		if (inputs.Count > 0)
		{
			var deviceName = Input.GetJoyName(inputs.First()).ToLower();
			_buttonKey.Text = deviceName.Contains("nintendo") ? "B" : deviceName.Contains("xinput") ? "A" : "X";
		}
		else
		{
			_buttonKey.Text = "Z";
		}
	}

	private void OnTimeoutRestoreDash()
	{
		_isDashAllowed = true;
		_dashContainer.Modulate = Colors.White;
	}

	public override void _Process(double delta)
	{
		CheckUserController();

		if (!_gameplayManager.IsGameplayActive()) return;
		
		if (_inputSystem.IsDashPressed() && _isDashAllowed)
		{
			_OnDashPressed();
		}
	}

	private void _OnDashPressed()
	{
		_isDashAllowed = false;
		_timer.Start();
		EmitSignal(SignalName.OnDashStarted);
		_dashContainer.Modulate = Colors.Gray;
	}
}