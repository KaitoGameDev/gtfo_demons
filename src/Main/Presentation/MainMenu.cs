using Godot;
using System;

public partial class MainMenu : Node
{
	private Button StartGameBtn;
	private Button CreditsBtn;
	private Button QuitBtn;
	public override void _Ready()
	{
		StartGameBtn = GetNode<Button>("CanvasLayer/VBoxContainer/StartGameBtn");
		CreditsBtn = GetNode<Button>("CanvasLayer/VBoxContainer/CreditsBtn");
		QuitBtn = GetNode<Button>("CanvasLayer/VBoxContainer/QuitBtn");
		
		StartGameBtn.GrabFocus();

		StartGameBtn.Connect("pressed", Callable.From(OnStartGamePressed));
		CreditsBtn.Connect("pressed", Callable.From(OnCreditsPressed));
		QuitBtn.Connect("pressed", Callable.From(OnQuitPressed));
	}

	private void OnQuitPressed()
	{
		GetTree().Quit();
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
