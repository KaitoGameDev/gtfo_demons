using Godot;

namespace gtfo_demons.InputSystem;

public class InputSystem: IInputSystem
{
    public Vector2 GetMovementDirection()
    {
        return Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    }

    public bool IsGrabPressed()
    {
        return Input.IsActionJustPressed("Grab");
    }

    public bool IsDashPressed()
    {
        return Input.IsActionJustPressed("Dash");
    }
}