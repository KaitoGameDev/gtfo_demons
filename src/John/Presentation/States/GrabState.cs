using Godot;
using gtfo_demons.StateMachine;

namespace gtfo_demons.John.Presentation.States;

public partial class GrabState: State
{
    [Export] private IdleState _idleState;
    [Export] private AnimatedSprite3D _animatedSprite;

    public override void OnEnter()
    {
        _animatedSprite.Play("grab");
        _animatedSprite.Connect(AnimatedSprite3D.SignalName.AnimationFinished, Callable.From(OnAnimationFinished));
    }

    private void OnAnimationFinished()
    {
        EmitSignal(State.SignalName.OnChangeState, _idleState);
    }

    public override void OnExit()
    {
        _animatedSprite.Disconnect(AnimatedSprite3D.SignalName.AnimationFinished, Callable.From(OnAnimationFinished));
    }
}