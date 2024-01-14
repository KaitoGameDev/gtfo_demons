using Godot;
using System;

public partial class John : CharacterBody2D
{
	[Export] private float Speed = 300.0f;
	[Export] private float DashImpulse = 500.0f;

	private AnimationPlayer _animationPlayer;
	private Node2D _pivot;
	private Vector2 _direction;
	private bool _isLookingLeft = true;
	private bool _dash = false;
	private Sprite2D _rake;

	public override void _Ready()
	{
		_rake = GetNode<Sprite2D>("PivotSprites/Rake");
		_rake.Visible = false;
		_animationPlayer = GetNode<AnimationPlayer>("Animator");
		_pivot = GetNode<Node2D>("PivotSprites");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Grab"))
		{
			_animationPlayer.Play("GrabDemon");
		}

		if (Input.IsActionJustPressed("Dash"))
		{
			_animationPlayer.Play("Dash");
			_dash = true;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		CheckTurnAround();

		_direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (_direction != Vector2.Zero)
		{
			velocity.X = _direction.X * Speed;
			velocity.Y = _direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		if (_dash)
		{
			_dash = false;
			velocity.X *=  DashImpulse;
			velocity.Y *=  DashImpulse;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void CheckTurnAround()
	{
		if (IsTurningToRight() || IsTurningToLeft())
		{
			_isLookingLeft = !_isLookingLeft;
			_pivot.Scale = new Vector2(_pivot.Scale.X * -1, 1);
		}
	}

	private bool IsTurningToRight()
	{
		return _isLookingLeft && _direction.X > 0;
	}

	private bool IsTurningToLeft()
	{
		return !_isLookingLeft && _direction.X < 0;
	}
}
