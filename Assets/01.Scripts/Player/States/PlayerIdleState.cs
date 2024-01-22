using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.MoveSpeed = _player.WalkSpeed;
    }

    public override void UpdateState()
    {
        var movementInput = _player.InputReader.MovementInput;
        if (movementInput.sqrMagnitude > 0.05f)
        {

            if (_player.InputReader.IsShiftPressed)
                _stateMachine.ChangeState(PlayerStateType.Run);
            else
                _stateMachine.ChangeState(PlayerStateType.Walk);

        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
