using Godot;
using gtfo_demons.Demons.Presentation;

namespace gtfo_demons.MollysHouse.Presentation;

public partial class LevelBuildings : StaticBody3D
{
    [Signal] public delegate void OnDamageReceivedEventHandler(float damage);
    
    [Export] private Area3D _hitBox;

    public override void _Ready()
    {
        _hitBox.Connect(Area3D.SignalName.BodyEntered, Callable.From<Node3D>(OnEnemyEntered));
    }

    private void OnEnemyEntered(Node3D body3d)
    {
        GD.Print($"AJA!! {body3d}");
        if (body3d is not Demon demon) return;
        GD.Print($"VAMOS!! {body3d}");
        EmitSignal(SignalName.OnDamageReceived, demon.GetDamageAmount());
        demon.CallDeferred(Node.MethodName.QueueFree);
    }
}