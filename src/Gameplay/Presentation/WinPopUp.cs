using Godot;

namespace gtfo_demons.Gameplay.Presentation; 

public partial class WinPopUp : Control {
	private Button _button;

	public override void _Ready() {
		_button = GetNode<Button>("Panel/Button");

		_button.Connect(BaseButton.SignalName.Pressed, Callable.From(OnButtonPressed));
	}

	private void OnButtonPressed() {
		GetTree().ChangeSceneToFile("res://src/Main/Presentation/MainMenu.tscn");
	}
}