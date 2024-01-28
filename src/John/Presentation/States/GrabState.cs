using Godot;
using gtfo_demons.StateMachine;

namespace gtfo_demons.John.Presentation.States;

public partial class GrabState: State
{
    [Export] private IdleState _idleState;
    [Export] private AnimatedSprite3D _animatedSprite;
    [Export] private AnimationPlayer _animationPlayer;
    [Export] private RakeArea _rakeArea;

    public override void OnEnter()
    {
        _animatedSprite.Play("grab");
        _animationPlayer.Play("grab");
        _rakeArea?.Enable();
        _animatedSprite.Connect(AnimatedSprite3D.SignalName.AnimationFinished, Callable.From(OnAnimationFinished));
    }

    private void OnAnimationFinished()
    {
        EmitSignal(State.SignalName.OnChangeState, _idleState);
    }

    public override void OnExit()
    {
        _rakeArea?.Disable();  
        _animatedSprite.Disconnect(AnimatedSprite3D.SignalName.AnimationFinished, Callable.From(OnAnimationFinished));
    }
}