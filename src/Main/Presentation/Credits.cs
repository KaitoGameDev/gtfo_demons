using Godot;

namespace gtfo_demons.Main.Presentation; 

public partial class Credits : Node {
	private Button _button;

	public override void _Ready() {
		_button = GetNode<Button>("CanvasLayer/Control/Button");

		_button.Connect(BaseButton.SignalName.Pressed, Callable.From(OnButtonPressed));
		
		_button.GrabFocus();
	}

	private void OnButtonPressed() {
		GetTree().ChangeSceneToFile("res://src/Main/Presentation/MainMenu.tscn");
	}
}