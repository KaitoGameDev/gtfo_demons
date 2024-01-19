using Godot;
using System;

public partial class MainMenu : Node
{
	private Button StartGameBtn;
	private Button CreditsBtn;
	public override void _Ready()
	{
		StartGameBtn = GetNode<Button>("CanvasLayer/VBoxContainer/StartGameBtn");
		CreditsBtn = GetNode<Button>("CanvasLayer/VBoxContainer/CreditsBtn");
		
		StartGameBtn.GrabFocus();

		StartGameBtn.Connect("pressed", Callable.From(OnStartGamePressed));
		CreditsBtn.Connect("pressed", Callable.From(OnCreditsPressed));
	}

	private void OnCreditsPressed()
	{
		GD.Print("CREDITS PRESSED");
	}

	private void OnStartGamePressed()
	{
		var mollysHouse = ResourceLoader.Load<PackedScene>("res://src/MollysHouse/Presentation/MollysHouse.tscn");
		GetTree().ChangeSceneToPacked(mollysHouse);
	}
}
