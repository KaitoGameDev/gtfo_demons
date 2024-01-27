using Godot;

namespace gtfo_demons.InputSystem;

public interface IInputSystem
{
    public Vector2 GetMovementDirection();
    public bool IsGrabPressed();
    public bool IsDashPressed();
}