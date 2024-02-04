using System;
using System.Reactive;

namespace gtfo_demons.John.Domain;

public interface IPlayerInteractions
{
    public void DashPressed();
    public void Overloaded();
    public bool IsInParadoxArea();
    public void SetParadoxArea(bool state);
    public IObservable<Unit> OnDashPressed();
    public IObservable<Unit> OnOverloaded();
}