using Godot;
using gtfo_demons.Gameplay.Presentation;
using gtfo_demons.InputSystem;
using gtfo_demons.StateMachine;

namespace gtfo_demons.John.Presentation.States;

public partial class IdleState : State
{
    [ExportGroup("Node State Requirements")] [Export]
    private CharacterBody3D _characterBody3D;

    [Export] private Node3D _body;
    [Export] private float _speed;
    [Export] private float _dashFactor;
    [Export] private Timer _dashTimer;
    [Export] private DashWidget _dashWidget;
    [Export] private AnimatedSprite3D _animatedSprite;

    [ExportGroup("States Related")] [Export]
    private GrabState _grabState;

    private Vector2 _direction = Vector2.Zero;
    private Vector3 _velocity = Vector3.Zero;

    public override void OnEnter()
    {
        _animatedSprite.Play("idle");
        HandleConnections();
    }

    private void HandleConnections()
    {
        _dashTimer.Connect("timeout", Callable.From(OnDashEnded));
        _dashWidget?.Connect(DashWidget.SignalName.OnDashStarted, Callable.From(OnDashStarted));
    }

    private void OnDashEnded()
    {
        _speed /= _dashFactor;
    }

    private void OnDashStarted()
    {
        _speed *= _dashFactor;
        _dashTimer.Start();
    }

    public override void HandleInput(double delta, IInputSystem inputSystem)
    {
        if (inputSystem.IsGrabPressed() && _grabState != null)
        {
            EmitSignal(State.SignalName.OnChangeState, _grabState);
            return;
        }

        _direction = inputSystem.GetMovementDirection();
        CheckCharacterOrientation();
    }

    private void CheckCharacterOrientation()
    {
        if (_direction.X > 0 && _body.RotationDegrees.Y >= 0)
        {
            _body.RotationDegrees = new Vector3(0f, -180f, 0f);
        }

        if (_direction.X < 0 && _body.RotationDegrees.Y < 0)
        {
            _body.RotationDegrees = new Vector3(0f, 0f, 0f);
        }
    }

    public override void Update(double delta)
    {
        _velocity = _characterBody3D.Velocity;

        if (_direction != Vector2.Zero)
        {
            _velocity.X = _direction.X * _speed;
            _velocity.Z = _direction.Y * _speed;
        }
        else
        {
            _velocity.X = Mathf.MoveToward(_velocity.X, 0, _speed);
            _velocity.Z = Mathf.MoveToward(_velocity.Z, 0, _speed);
        }

        _characterBody3D.Velocity = _velocity;
        _characterBody3D.MoveAndSlide();
    }

    public override void OnExit()
    {
        _dashTimer?.Disconnect(Timer.SignalName.Timeout, Callable.From(OnDashEnded));
        _dashWidget?.Disconnect(DashWidget.SignalName.OnDashStarted, Callable.From(OnDashStarted));
    }
}