using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Godot;

namespace gtfo_demons.Gameplay.Presentation;

public partial class DashWidget : Control
{
	private Panel _dashContainer;
	private Timer _timer;
	private readonly Subject<Unit> _emitter = new();
	private Label _buttonKey;

	private bool _isDashAllowed = true;
	private IGameplayManager _gameplayManager;
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
		
		if (Input.IsActionJustPressed("Dash") && _isDashAllowed)
		{
			_OnDashPressed();
		}
	}

	private void _OnDashPressed()
	{
		_isDashAllowed = false;
		_timer.Start();
		_emitter.OnNext(Unit.Default);
		_dashContainer.Modulate = Colors.Gray;
	}

	public IObservable<Unit> OnDashPressed() => _emitter.AsObservable();
}