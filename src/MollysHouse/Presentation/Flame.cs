using Godot;
using System;

public partial class Flame : Sprite3D
{
	[Export] private bool _playBackward;

	private AnimationPlayer _player;
	public override void _Ready() {
		_player = GetNode<AnimationPlayer>("AnimationPlayer");
		if (_playBackward) {
			_player.PlayBackwards("flame");
		}
		else {
			_player.Play("flame");
		}
	}
}
