using Godot;
using gtfo_demons.InputSystem;

namespace gtfo_demons.StateMachine;

public partial class State: Node
{
    [Signal] public delegate void OnChangeStateEventHandler(State state);
    public virtual void OnEnter() {}
    public virtual void OnExit() {}
    public virtual void HandleInput(double delta, IInputSystem inputSystem){}
    public virtual void Update(double delta){}
}