using System;
using Godot;

namespace gtfo_demons.Gameplay;

public interface IGameplayManager
{
    public bool IsGameplayActive();
    public void PauseGameplay();
    public void ResumeGameplay();
    public IObservable<bool> OnGameplayChanged();
}