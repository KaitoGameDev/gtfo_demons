using Godot;
using System;
using System.Reactive;
using gtfo_demons.Gameplay;
using gtfo_demons.Gameplay.Presentation;

public partial class John : CharacterBody3D
{
    private IGameplayManager _gameplayManager;
    private AnimationPlayer _animator;
    private Timer _dashTimer;

    [Export] private float Speed = 5.0f;
    [Export] private float DashFactor = 5.0f;
    [Export] private DashWidget _dashWidget;

    private Vector2 _direction = Vector2.Zero;
    private Vector3 _velocity = Vector3.Zero;

    private bool _dashing = false;

    public override void _Ready()
    {
        _gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
        _animator = GetNode<AnimationPlayer>("Animator");
        _animator.Play("Idle");
        _dashTimer = GetNode<Timer>("DashTime");
        _dashTimer.Connect("timeout", Callable.From(OnDashTimeout));
        _dashWidget?.OnDashPressed().Subscribe(OnDashPressed);
    }

    private void OnDashTimeout()
    {
        _dashing = false;
    }

    private void OnDashPressed(Unit _)
    {
        _dashing = true;
        _dashTimer.Start();
    }
    
    public override void _Process(double delta)
    {
        if (!_gameplayManager.IsGameplayActive())
        {
            _direction = Vector2.Zero;
            return;
        }

        _direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    }

    public override void _PhysicsProcess(double delta)
    {
        _velocity = Velocity;

        if (_direction != Vector2.Zero)
        {
            if (_dashing)
            {
                _velocity.X = _direction.X * Speed * DashFactor;
                _velocity.Z = _direction.Y * Speed * DashFactor;
            }
            else
            {
                _velocity.X = _direction.X * Speed;
                _velocity.Z = _direction.Y * Speed;   
            }
        }
        else
        {
            _velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            _velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = _velocity;
        MoveAndSlide();
    }
}