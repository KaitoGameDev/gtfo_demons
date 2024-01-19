using System;
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

	private bool _isDashAllowed = true;
	private IGameplayManager _gameplayManager;
	public override void _Ready()
	{
		_gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
		_timer = GetNode<Timer>("Timer");
		_timer.Connect("timeout", Callable.From(OnTimeoutRestoreDash));
		_dashContainer = GetNode<Panel>("Panel");
	}

	private void OnTimeoutRestoreDash()
	{
		_isDashAllowed = true;
		_dashContainer.Modulate = Colors.White;
	}

	public override void _Process(double delta)
	{
		if (!_gameplayManager.IsGameplayActive()) return;
		
		if (Input.IsActionJustPressed("Dash") && _isDashAllowed)
		{
			OnDashPressed();
		}
	}

	private void OnDashPressed()
	{
		_isDashAllowed = false;
		_timer.Start();
		_emitter.OnNext(Unit.Default);
		_dashContainer.Modulate = Colors.Gray;
	}

	public IObservable<Unit> OnDashStatusChanged() => _emitter.AsObservable();
}