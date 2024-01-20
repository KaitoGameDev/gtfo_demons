using Godot;
using System;

public partial class ParadoxBox : StaticBody3D
{
	private Area3D _workableArea;
	private AnimationPlayer _animator;
	public override void _Ready()
	{
		_workableArea = GetNode<Area3D>("Area3D");
		_animator = GetNode<AnimationPlayer>("Animator");
		_workableArea.Connect("body_entered", Callable.From<Node3D>(OnPlayerEnter));
		_workableArea.Connect("body_exited", Callable.From<Node3D>(OnPlayerLeave));
	}

	private void OnPlayerLeave(Node3D body)
	{
		if (body is not John) return;
		_animator.PlayBackwards("open");
	}

	private void OnPlayerEnter(Node3D body)
	{
		if (body is not John) return;
		_animator.Play("open");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
