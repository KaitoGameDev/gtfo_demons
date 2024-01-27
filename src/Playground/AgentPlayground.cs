using Godot;
using System;

public partial class AgentPlayground : CharacterBody3D
{
	[Export] private Node3D _target;
	
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	private NavigationAgent3D _agent;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_agent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		// _agent.TargetPosition = _target.GlobalPosition;
		_agent.TargetPosition = new Vector3(76, 0, 0);
		SetPhysicsProcess(false);
		GetTree().CreateTimer(2.0f).Connect(Timer.SignalName.Timeout, Callable.From(Go));
	}

	public void Go ()
	{
		SetPhysicsProcess(true);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		var direction = (_agent.GetNextPathPosition() - GlobalPosition).Normalized();
		GD.Print($"DIR: {direction}");
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
