using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        var movementInput = Quaternion.Euler(0, 45, 0) * _player.InputReader.MovementInput; //자연스러운 쿼터뷰 연출을 위한 45도 돌리기

        var movementSpeed = _player.MoveSpeed * Time.deltaTime;
        _player.SetVelocity(movementInput * movementSpeed);
        _player.Rotate(movementInput);

        if (movementInput.sqrMagnitude < 0.05f)
            _stateMachine.ChangeState(PlayerStateType.Idle);

        if (_player.InputReader.IsShiftPressed)
            _stateMachine.ChangeState(PlayerStateType.Run);

        _player.AnimatorController.SetSpeed(_player.MoveSpeed);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
