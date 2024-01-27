using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace gtfo_demons.Demons.Presentation;

public partial class DemonPortals : Node
{
    [Export] private Timer _summonTime;
    [Export] private Array<PackedScene> _availableDemons;
    [Export(PropertyHint.Range, "1,5,1")] private int _minSummonIntervalInSeg;
    [Export(PropertyHint.Range, "5,10,1")] private int _maxSummonIntervalInSeg;
    [Export] private Node3D _target;
    private List<DemonPortal> _portals;

    public override void _Ready()
    {
        _portals = GetNode<Node3D>("Portals")
            .GetChildren()
            .ToList()
            .Select(node => node as DemonPortal)
            .ToList();
        
        _summonTime.Connect(Timer.SignalName.Timeout, Callable.From(OnSummon));
        _summonTime.Start();
    }

    private void OnSummon()
    {
        var portalIdx = GD.RandRange(0, _portals.Count - 1);
        var portal = _portals[portalIdx];
        
        var demonScene = _availableDemons[GD.RandRange(0, _availableDemons.Count - 1)];
        var demon = demonScene.Instantiate<Demon>();
       
        demon.SetTarget(_target);
        demon.Position = portal.GlobalPosition;
        GetParent().AddChild(demon);
        
        _summonTime.WaitTime = GD.RandRange(_minSummonIntervalInSeg, _maxSummonIntervalInSeg);
        _summonTime.Start();
    }
}