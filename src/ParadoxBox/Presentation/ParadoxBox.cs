using Godot;
using gtfo_demons.John.Domain;
using gtfo_demons.John.Factory;

namespace gtfo_demons.ParadoxBox.Presentation;

public partial class ParadoxBox : Node3D
{
	private Area3D _workableArea;
	private AnimationPlayer _animator;

	private IPlayerInteractions _playerInteractions;
	public override void _Ready()
	{
		_workableArea = GetNode<Area3D>("Area3D");
		_animator = GetNode<AnimationPlayer>("Animator");
		_workableArea.Connect(Area3D.SignalName.BodyEntered, Callable.From<Node3D>(OnPlayerEnter));
		_workableArea.Connect(Area3D.SignalName.BodyExited, Callable.From<Node3D>(OnPlayerLeave));
		_playerInteractions = PlayerServicesFactory.GetPlayerInteractions();
	}

	private void OnPlayerLeave(Node3D body)
	{
		if (body is not gtfo_demons.John.Presentation.John) return;
		_playerInteractions.SetParadoxArea(false);
		_animator.PlayBackwards("open");
	}

	private void OnPlayerEnter(Node3D body)
	{
		if (body is not gtfo_demons.John.Presentation.John) return;
		_playerInteractions.SetParadoxArea(true);
		_animator.Play("open");
	}
}