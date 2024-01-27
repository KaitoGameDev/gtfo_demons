using Godot;

namespace gtfo_demons.ParadoxBox.Presentation;

public partial class ParadoxBox : Node3D
{
	private Area3D _workableArea;
	private AnimationPlayer _animator;
	public override void _Ready()
	{
		_workableArea = GetNode<Area3D>("Area3D");
		_animator = GetNode<AnimationPlayer>("Animator");
		_workableArea.Connect(Area3D.SignalName.BodyEntered, Callable.From<Node3D>(OnPlayerEnter));
		_workableArea.Connect(Area3D.SignalName.BodyExited, Callable.From<Node3D>(OnPlayerLeave));
	}

	private void OnPlayerLeave(Node3D body)
	{
		if (body is not gtfo_demons.John.Presentation.John) return;
		_animator.PlayBackwards("open");
	}

	private void OnPlayerEnter(Node3D body)
	{
		if (body is not gtfo_demons.John.Presentation.John) return;
		_animator.Play("open");
	}
}