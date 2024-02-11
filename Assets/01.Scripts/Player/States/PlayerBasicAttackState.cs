using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAttackState : PlayerState
{
    public PlayerBasicAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        var dir = Quaternion.Euler(0, 45, 0) * _player.InputReader.MovementInput;
        _player.Rotate(dir, false);

        if (_player.CurrentComboCounter >= 2) //¸¶Áö¸· ÄÞº¸¸¸ µô·¹ÀÌ ´Ã·ÁÁÜ
            _player.AttackDelayTime = 1.25f;
        else
            _player.AttackDelayTime = 0.75f;

        _player.AnimatorController.SetAttackCount(_player.CurrentComboCounter);
        _player.AnimatorController.SetAttack(true);
        _player.CurrentComboCounter++;

        _player.DashCoroutine(dir);

        _player.CanAttack = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_player.CanAttack && _player.AttackTimer < _player.AttackDelayTime) 
        {
            _player.AttackTimer += Time.deltaTime;
            if (_player.AttackTimer >= _player.AttackDelayTime)
            {
                _player.CanAttack = true;
                _player.CurrentComboCounter = 0;
                _player.AnimatorController.SetAttackCount(_player.CurrentComboCounter);
                _stateMachine.ChangeState(PlayerStateType.Idle);
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.AnimatorController.SetAttack(false);
    }
}
