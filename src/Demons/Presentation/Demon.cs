using Godot;
using gtfo_demons.Gameplay;

namespace gtfo_demons.Demons.Presentation;

public partial class Demon: CharacterBody3D
{
    [Export] private float _speed = 5.0f;
    [Export] private Node3D _target;
    [Export] private float _damageAmount;

    private IGameplayManager _gameplayManager;

    public override void _Ready() {
        _gameplayManager = GameplayFactory.GetPlayerManager();
    }

    public void SetTarget(Node3D target)
    {
        _target = target;
    }

    public override void _Process(double delta) {
        if (!_gameplayManager.IsGameplayActive()) {
            CallDeferred(Node.MethodName.QueueFree);
        }
    }

    public override void _PhysicsProcess(double delta) {
        if (_target == null) return;
        
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
            velocity.Y -= 9.8f * (float)delta;

        var direction = (_target.GlobalPosition - GlobalPosition).Normalized();
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

    public void Grabbed() {
        _target = null;
    }
}