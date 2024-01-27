using Godot;
using gtfo_demons.InputSystem;

namespace gtfo_demons.StateMachine;

public partial class StateMachine: Node
{
    [Export] private State _initialState;

    private State _currentState;
    private IInputSystem _inputSystem;

    public override void _Ready()
    {
        _inputSystem = new InputSystem.InputSystem();
        OnStateChanged(_initialState);
    }

    public override void _Process(double delta)
    {
        _currentState.HandleInput(delta, _inputSystem);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState.Update(delta);
    }

    private void OnStateChanged(State newState)
    {
        if (newState != null && _currentState == newState) return;
        _currentState?.Disconnect(State.SignalName.OnChangeState, Callable.From<State>(OnStateChanged));
        _currentState?.OnExit();
        _currentState = newState;
        _currentState?.OnEnter();
        _currentState?.Connect(State.SignalName.OnChangeState, Callable.From<State>(OnStateChanged));
    }
}