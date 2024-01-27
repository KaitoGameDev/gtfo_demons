using Godot;

namespace gtfo_demons.Gameplay.Presentation;

public partial class FollowingCamera : Camera3D
{
	[Export] private Node3D _target;
	[Export] private float _smoothness;
	[Export] private Vector3 _offset;

	public override void _Process(double delta)
	{
		if (_target == null) return;
		var position = _target.GlobalPosition - GlobalPosition +_offset;
		
		Translate(GlobalPosition.Lerp(position, _smoothness));
	}
}