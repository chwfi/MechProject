using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.MoveSpeed = _player.RunSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_player.InputReader.IsShiftPressed)
            _stateMachine.ChangeState(PlayerStateType.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();

        _player.MoveSpeed = _player.RunSpeed;
    }
}
