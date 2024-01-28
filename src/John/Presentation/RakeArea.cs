using Godot;
using gtfo_demons.Demons.Presentation;

namespace gtfo_demons.John.Presentation; 

public partial class RakeArea: Area3D {
    private Demon _demon;
    [Export] private Node3D _rakePoint;

    public override void _Ready() {
        Monitoring = false;
        Connect(Area3D.SignalName.BodyEntered, Callable.From<Node3D>(OnBodyEntered));
    }

    public void Enable() {
        Monitoring = true;
    }

    public void Disable() {
        Monitoring = false;
        _demon?.QueueFree();
        _demon = null;  
    }
    
    private void OnBodyEntered(Node3D body) {
        if (_demon != null) return;
        if (body is not Demon demon) return;
        _demon = demon;
        _demon.Grabbed();
        _demon.Reparent(_rakePoint);
        _demon.Position = Vector3.Zero;
    }
}