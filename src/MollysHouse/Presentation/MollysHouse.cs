using Godot;
using gtfo_demons.Gameplay;
using gtfo_demons.Gameplay.Presentation;

namespace gtfo_demons.MollysHouse.Presentation; 

public partial class MollysHouse : Node3D {
	[Export] private BuildingHealthBar _buildingHealthBar;
	[Export] private Control _gameOverPopUp;
	[Export] private Control _pausePopUp;
	
	private IGameplayManager _gameplayManager;
	public override void _Ready()
	{
		_gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
		_gameplayManager.ResumeGameplay();

		_buildingHealthBar.Connect(BuildingHealthBar.SignalName.OnBuildingDestroyed,
			Callable.From(OnBuildingDestroyed));
	}

	private void OnBuildingDestroyed() {
		_gameplayManager.PauseGameplay();
		_gameOverPopUp.Visible = true;
	}
}