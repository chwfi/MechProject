using UnityEngine;

public class State
{
    protected StateMachine _stateMachine;

    protected int _animToggleHash;
    protected bool _animFinishTriggerCalled;
    protected Entity _owner;

    public State(StateMachine stateMachine, Entity owner, System.Enum type)
    {
        _stateMachine = stateMachine;
        _owner = owner;
        _animToggleHash = Animator.StringToHash(type.ToString());
    }

    public virtual void EnterState()
    {
        _owner.AnimatorCompo.SetBool(_animToggleHash, true);
    }

    public virtual void UpdateState()
    {
    }

    public virtual void ExitState()
    {
        _owner.AnimatorCompo.SetBool(_animToggleHash, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _animFinishTriggerCalled = true;
    }
}