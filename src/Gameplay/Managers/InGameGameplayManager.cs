using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Godot;

namespace gtfo_demons.Gameplay.Managers;

public partial class InGameGameplayManager : Node, IGameplayManager
{
    private static bool _isActive;
    private readonly ISubject<bool> _emitter = new Subject<bool>();

    public bool IsGameplayActive() => _isActive;

    public void PauseGameplay()
    {
        _isActive = false;
        _emitter.OnNext(_isActive);
    }

    public void ResumeGameplay()
    {
        _isActive = true;
        _emitter.OnNext(_isActive);
    }

    public IObservable<bool> OnGameplayChanged() => _emitter.AsObservable();
}