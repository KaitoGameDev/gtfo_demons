using Godot;
using System;
using gtfo_demons.Gameplay;

public partial class MollysHouse : Node3D
{
	private IGameplayManager _gameplayManager;
	public override void _Ready()
	{
		_gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
		GD.Print($"House ${_gameplayManager}");
		_gameplayManager.ResumeGameplay();
	}
}
