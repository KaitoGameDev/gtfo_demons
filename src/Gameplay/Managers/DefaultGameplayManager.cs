using System;
using System.Reactive.Subjects;

namespace gtfo_demons.Gameplay;

public class DefaultGameplayManager: IGameplayManager
{
    private readonly ISubject<bool> _subject = new Subject<bool>();
    private bool _isActive = true;

    public bool IsGameplayActive() => _isActive;

    public void PauseGameplay()
    {
        _isActive = false;
        _subject.OnNext(_isActive);
    }

    public void ResumeGameplay()
    {
        _isActive = true;
        _subject.OnNext(_isActive);
    }

    public IObservable<bool> OnGameplayChanged() => _subject;
}