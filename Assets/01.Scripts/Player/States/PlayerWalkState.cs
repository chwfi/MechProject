using System;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        var movementInput = Quaternion.Euler(0, 45, 0) * _player.InputReader.MovementInput; //�ڿ������� ���ͺ� ������ ���� 45�� ������

        if (movementInput.sqrMagnitude < 0.05f)
            _stateMachine.ChangeState(PlayerStateType.Idle);

        var movementSpeed = _player.MoveSpeed * Time.deltaTime;
        _player.SetVelocity(movementInput * movementSpeed);
        _player.Rotate(movementInput);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
