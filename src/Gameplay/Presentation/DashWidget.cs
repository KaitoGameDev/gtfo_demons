using System.Linq;
using Godot;
using gtfo_demons.InputSystem;
using gtfo_demons.John.Domain;
using gtfo_demons.John.Factory;

namespace gtfo_demons.Gameplay.Presentation;

public partial class DashWidget : Control
{
	
	private Panel _dashContainer;
	private Timer _timer;
	private Label _buttonKey;
	private IGameplayManager _gameplayManager;
	private IPlayerInteractions _playerInteractions;

	private bool _isDashAllowed = true;
	private readonly IInputSystem _inputSystem = new InputSystem.InputSystem();
	public override void _Ready() {
		_gameplayManager = GameplayFactory.GetPlayerManager();
		_playerInteractions = PlayerServicesFactory.GetPlayerInteractions();
		
		LoadInternalNodes();
	}

	private void LoadInternalNodes() {
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
		_playerInteractions.DashPressed();
		_dashContainer.Modulate = Colors.Gray;
	}
}