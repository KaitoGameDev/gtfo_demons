using Godot;
using System;
using gtfo_demons.Gameplay;

public partial class John : CharacterBody3D
{
    private IGameplayManager _gameplayManager;
    private AnimationPlayer _animator;

    [Export] private const float Speed = 5.0f;

    private Vector2 _direction = Vector2.Zero;
    private Vector3 _velocity = Vector3.Zero;

    public override void _Ready()
    {
        _gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
        GD.Print($"John ${_gameplayManager}");
        _animator = GetNode<AnimationPlayer>("Animator");
        _animator.Play("Idle");
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
            _velocity.X = _direction.X * Speed;
            _velocity.Z = _direction.Y * Speed;
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