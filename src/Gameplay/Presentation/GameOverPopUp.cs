using Godot;

namespace gtfo_demons.Gameplay.Presentation;

public partial class GameOverPopUp : Control {
    [Export] private string _buildingName = "BUILDING NAME";
    [Export] private ResourceImporterScene _level;

    private Button _backToMainBtn;
    private Button _retryBtn;
    private Label _title;

    public override void _Ready() {
        GD.Print("ON READY");
        _title = GetNode<Label>("Panel/Title");
        _backToMainBtn = GetNode<Button>("Panel/HBoxContainer/MainMenuBtn");
        _retryBtn = GetNode<Button>("Panel/HBoxContainer/RetryBtn");
        
        _backToMainBtn.Connect(BaseButton.SignalName.Pressed, Callable.From(OnBackToMainPressed));
        _retryBtn.Connect(BaseButton.SignalName.Pressed, Callable.From(OnRetryPressed));

        Connect(CanvasItem.SignalName.VisibilityChanged, Callable.From(OnVisibleChanged));
        
        _title.Text = _buildingName + " " + _title.Text;
    }

    private void OnVisibleChanged() {
        _retryBtn.GrabFocus();
    }

    private void OnRetryPressed() {
        GetTree().ChangeSceneToFile(_level.);
    }

    private void OnBackToMainPressed() {
        GetTree().ChangeSceneToFile("res://src/Main/Presentation/MainMenu.tscn");
    }
}