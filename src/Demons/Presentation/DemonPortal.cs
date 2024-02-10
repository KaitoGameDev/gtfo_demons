using Godot;
using Godot.Collections;
using gtfo_demons.Gameplay;

namespace gtfo_demons.Demons.Presentation;

public partial class DemonPortal : Node3D
{
	[Export] private Timer _summonTime;
	[Export] private Array<PackedScene> _availableDemons;
	[Export] private Node3D _target;

	private IGameplayManager _gameplayManager;
	
	public override void _Ready() {
		_summonTime.Connect(Timer.SignalName.Timeout, Callable.From(OnSummon));
		_summonTime.Start();

		_gameplayManager = GameplayFactory.GetPlayerManager();
	}

	private void OnSummon() {
		if (!_gameplayManager.IsGameplayActive()) return;
		
		var demonScene = _availableDemons[GD.RandRange(0, _availableDemons.Count - 1)];
		var demon = demonScene.Instantiate<Demon>();
       
		demon.SetTarget(_target);
		demon.Position = Position;
		GetParent().AddChild(demon);
        
		_summonTime.WaitTime = GD.RandRange(2, 8);
		_summonTime.Start();
	}
}