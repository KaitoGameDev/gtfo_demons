using System;
using System.Reactive.Subjects;

namespace gtfo_demons.Gameplay;

public class DefaultGameplayManager: IGameplayManager
{
    private IObservable<bool> _ = new Subject<bool>();
    private bool _isActive = true;

    public bool IsGameplayActive() => _isActive;

    public void PauseGameplay()
    {
        _isActive = false;
    }

    public void ResumeGameplay()
    {
        _isActive = true;
    }

    public IObservable<bool> OnGameplayChanged() => _;
}