using System;
using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
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
        base.UpdateState();

        _player.MoveSpeed = Mathf.Lerp(_player.MoveSpeed, _player.WalkSpeed, Time.deltaTime * _player.LerpValue);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
