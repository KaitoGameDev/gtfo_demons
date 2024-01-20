using Godot;
using System;
using System.Reactive;
using gtfo_demons.Gameplay;
using gtfo_demons.Gameplay.Presentation;

public partial class John : CharacterBody3D
{
    private IGameplayManager _gameplayManager;
    private Timer _dashTimer;
    private Timer _grabTimer;
    private AnimationTree _animationTree;
    private Node3D _body;

    [Export] private float Speed = 5.0f;
    [Export] private float DashFactor = 5.0f;
    [Export] private DashWidget _dashWidget;

    private Vector2 _direction = Vector2.Zero;
    private Vector3 _velocity = Vector3.Zero;

    private bool _dashing;
    private bool _grabbing;

    public override void _Ready()
    {
        _body = GetNode<Node3D>("Sprites");
        _gameplayManager = GameplayFactory.GetGameplayManagerOrDefault(GetTree());
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _dashTimer = GetNode<Timer>("DashTime");
        _grabTimer = GetNode<Timer>("GrabTimer");
        _dashTimer.Connect("timeout", Callable.From(OnDashTimeout));
        _grabTimer.Connect("timeout", Callable.From(OnGrabTimeout));
        _dashWidget?.OnDashPressed().Subscribe(OnDashPressed);
    }

    private void OnGrabTimeout()
    {
        _grabbing = false;
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

        CaptureMovingAction();
        CaptureGrabAction();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _animationTree.Set("parameters/conditions/idle", !_grabbing);
        _animationTree.Set("parameters/conditions/grabbing", _grabbing);
    }

    private void CaptureMovingAction()
    {
        _direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (_direction.X > 0 && _body.RotationDegrees.Y >= 0)
        {
            _body.RotationDegrees = new Vector3(0f, -180f, 0f);
        }

        if (_direction.X < 0 && _body.RotationDegrees.Y < 0)
        {
            _body.RotationDegrees = new Vector3(0f, 0f, 0f);
        }
    }

    private void CaptureGrabAction()
    {
        if (Input.IsActionJustPressed("Grab") && !_grabbing)
        {
            _grabbing = true;
            _grabTimer.Start();
        }
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