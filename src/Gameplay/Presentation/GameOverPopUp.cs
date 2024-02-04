using Godot;
using gtfo_demons.MollysHouse.Presentation;

namespace gtfo_demons.Gameplay.Presentation;

public partial class GameOverPopUp : Control {
    [Export] private string _buildingName = "BUILDING NAME";
    [Export] private string _level;

    private Button _backToMainBtn;
    private Button _retryBtn;
    private Label _title;

    private GameOverReason _reason = GameOverReason.BuildingDestroyed;

    public void SetReason(GameOverReason reason) {
        _reason = reason;
    }

    public override void _Ready() {
        _title = GetNode<Label>("Panel/Title");
        _backToMainBtn = GetNode<Button>("Panel/HBoxContainer/MainMenuBtn");
        _retryBtn = GetNode<Button>("Panel/HBoxContainer/RetryBtn");
        
        _backToMainBtn.Connect(BaseButton.SignalName.Pressed, Callable.From(OnBackToMainPressed));
        _retryBtn.Connect(BaseButton.SignalName.Pressed, Callable.From(OnRetryPressed));

        Connect(CanvasItem.SignalName.VisibilityChanged, Callable.From(OnVisibleChanged));
    }

    private void OnVisibleChanged() {
        if (_reason == GameOverReason.Overloaded) {
            _title.Text = "Your Backpack was Overloaded of demon's souls, be careful next time";
        }

        if (_reason == GameOverReason.BuildingDestroyed) {
            _title.Text = $"{_buildingName} Has been destroyed!";
        }
        _retryBtn.GrabFocus();
    }

    private void OnRetryPressed() {
        GetTree().ChangeSceneToFile(_level);
    }

    private void OnBackToMainPressed() {
        GetTree().ChangeSceneToFile("res://src/Main/Presentation/MainMenu.tscn");
    }
}