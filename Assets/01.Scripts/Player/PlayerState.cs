using System;
using UnityEngine;

public class PlayerState : State
{
    protected Player _player => _owner as Player;

    public PlayerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        _player.AnimatorController.OnAnimationEndTrigger += OnAnimationEnd;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();

        _player.AnimatorController.OnAnimationEndTrigger -= OnAnimationEnd;
    }

    private void OnAnimationEnd()
    {
        _player.CanAttack = true;
        _player.AttackTimer = 0;
    }
}