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

        _player.PlayerEffectManager.BladeEffect();

        Quaternion playerRotation = _player.AnimatorController.transform.rotation; //Visual의 회전값을가져온다
        Vector3 forwardDirection = playerRotation * Vector3.forward;
        _player.Dash(forwardDirection, _player.AttackMoveDelay, _player.AttackMoveTime, _player.AttackMoveSpeed);
        _player.Rotate(forwardDirection, false);

        if (_player.CurrentComboCounter >= 2) //마지막 콤보만 딜레이 늘려줌
            _player.AttackDelayTime = 1.25f;
        else
            _player.AttackDelayTime = 0.75f;

        _player.AnimatorController.SetAttackCount(_player.CurrentComboCounter);
        _player.AnimatorController.SetAttack(true);
        _player.CurrentComboCounter++;

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
