using System;
using System.Reactive;
using System.Reactive.Subjects;
using gtfo_demons.John.Domain;

namespace gtfo_demons.John.Infrastructure;

public class PlayerInteractions: IPlayerInteractions
{
    private bool _isInParadoxArea;
    private readonly ISubject<Unit> _dashPressedInteraction = new Subject<Unit>();
    private readonly ISubject<Unit> _overloadedInteraction = new Subject<Unit>();

    public void DashPressed() {
        _dashPressedInteraction.OnNext(Unit.Default);
    }

    public void SetParadoxArea(bool state) {
        _isInParadoxArea = state;
    }

    public void Overloaded() {
        _overloadedInteraction.OnNext(Unit.Default);
    }

    public bool IsInParadoxArea() => _isInParadoxArea;

    public IObservable<Unit> OnDashPressed() => _dashPressedInteraction;
    
    public IObservable<Unit> OnOverloaded() => _overloadedInteraction;
}