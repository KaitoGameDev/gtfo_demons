using Godot;
using System;

public partial class Splash : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().CreateTimer(3.0).Connect("timeout", Callable.From(OnTimeout));
	}

	public void OnTimeout()
	{
		var mainMenu = ResourceLoader.Load<PackedScene>("res://src/Main/Presentation/MainMenu.tscn");
		GetTree().ChangeSceneToPacked(mainMenu);
	}
}
