using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Godot;
using gtfo_demons.Gameplay;
using gtfo_demons.Gameplay.Presentation;
using gtfo_demons.John.Domain;
using gtfo_demons.John.Factory;

namespace gtfo_demons.MollysHouse.Presentation;

public partial class MollysHouse : Node3D
{
    [Export] private BuildingHealthBar _buildingHealthBar;
    [Export] private GameOverPopUp _gameOverPopUp;
    [Export] private WaveTimer _waveTimer;
    [Export] private WinPopUp _winPopUp;

    private IGameplayManager _gameplayManager;
    private IPlayerInteractions _playerInteractions;

    private readonly CompositeDisposable _disposables = new();

    public override void _Ready() {
        _gameplayManager = GameplayFactory.GetPlayerManager();
        _playerInteractions = PlayerServicesFactory.GetPlayerInteractions();

        _gameplayManager.ResumeGameplay();

        _buildingHealthBar.Connect(BuildingHealthBar.SignalName.OnBuildingDestroyed,
            Callable.From(() => OnBuildingDestroyed(GameOverReason.BuildingDestroyed)));

        _waveTimer.Connect(WaveTimer.SignalName.OnWaveSurvived, Callable.From(OnWaveSurvived));

        _disposables.Add(
            _playerInteractions.OnOverloaded().Do(_ => OnBuildingDestroyed(GameOverReason.Overloaded)).Subscribe()
        );
    }

    private void OnWaveSurvived() {
        _gameplayManager.PauseGameplay();
        _winPopUp.Visible = true;
    }

    private void OnBuildingDestroyed(GameOverReason reason) {
        _gameplayManager.PauseGameplay();
        _gameOverPopUp.SetReason(reason);
        _gameOverPopUp.Visible = true;
    }

    public override void _ExitTree() {
        _disposables.Dispose();
    }
}

public enum GameOverReason
{
    Overloaded,
    BuildingDestroyed
}