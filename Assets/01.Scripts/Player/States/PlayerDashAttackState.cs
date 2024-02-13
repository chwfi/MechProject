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

        Quaternion playerRotation = _player.AnimatorController.transform.rotation; //Visual의 회전값을가져온다
        Vector3 forwardDirection = playerRotation * Vector3.forward;
        _player.Dash(forwardDirection, _player.DashDelay, _player.DashTime, _player.DashSpeed);

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
        _player.CurrentComboCounter = 0;
        _stateMachine.ChangeState(PlayerStateType.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.AnimatorController.OnDashAttackEndTrigger -= OnDashEnd;
        _player.AnimatorController.SetDashAttack(false);
    }
}
