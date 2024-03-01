using System;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.AnimatorController.SetRun(true);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //_player.MoveSpeed = _player.RunSpeed;

        if (!_player.InputReader.IsShiftPressed)
            _stateMachine.ChangeState(PlayerStateType.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();

        _player.AnimatorController.SetRun(false);
    }
}