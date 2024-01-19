using Godot;

namespace gtfo_demons.Main.Presentation;

public partial class BaseGameBtn : Button
{
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Accept") && HasFocus())
		{
			EmitSignal(BaseButton.SignalName.Pressed);
		}
	}
}