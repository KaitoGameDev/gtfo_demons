using Godot;

namespace gtfo_demons.Demons.Presentation;

public partial class Demon: CharacterBody3D
{
    [Export] private float _speed = 5.0f;
    [Export] private Node3D _target;
    [Export] private float _damageAmount;
    
    private NavigationAgent3D _agent;
    
    public void SetTarget(Node3D target)
    {
        _target = target;
    }
    public override void _Ready()
    {
        GD.Print($"DIR: {GlobalPosition}");
        _agent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        _agent.TargetPosition = _target.GlobalPosition;
        SetPhysicsProcess(false);
        GetTree().CreateTimer(2.0f).Connect(Timer.SignalName.Timeout, Callable.From(() =>
        {
            SetPhysicsProcess(true);
        }));
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= 9.8f * (float)delta;

        var direction = (_agent.GetNextPathPosition() - GlobalPosition).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * _speed;
            velocity.Z = direction.Z * _speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, _speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public float GetDamageAmount() => _damageAmount;
}