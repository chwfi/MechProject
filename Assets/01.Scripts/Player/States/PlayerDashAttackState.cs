using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttackState : PlayerState
{
    public PlayerDashAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.AnimatorController.OnDashAttackEndTrigger += OnDashEnd;

        _player.AnimatorController.SetDashAttack(true);
        _player.CanAttack = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void OnDashEnd()
    {
        _player.CanAttack = true;
        _stateMachine.ChangeState(PlayerStateType.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.AnimatorController.OnDashAttackEndTrigger -= OnDashEnd;
        _player.AnimatorController.SetDashAttack(false);
    }
}
