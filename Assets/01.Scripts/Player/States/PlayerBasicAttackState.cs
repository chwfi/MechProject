using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAttackState : PlayerState
{
    private float _keyDelay = 0.99f;
    private float _keyTimer = 0;

    public PlayerBasicAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _player.AnimatorController.OnAnimationEndTrigger += OnAnimationEnd;

        var dir = Quaternion.Euler(0, 45, 0) * _player.InputReader.MovementInput;
        _player.Rotate(dir);

        _player.AnimatorController.SetAttackCount(_player.CurrentComboCounter);
        _player.AnimatorController.SetAttackTrigger();
        _player.CurrentComboCounter++;

        _player.DashCoroutine(dir);

        _player.CanAttack = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_player.CanAttack && _keyTimer > 0) //시간이 다 되면 Idle로 되돌려놓는용도
        {
            _keyTimer -= Time.deltaTime;
            if (_keyTimer <= 0)
            {
                _player.CurrentComboCounter = 0;
                _player.AnimatorController.SetAttackCount(_player.CurrentComboCounter);
                _stateMachine.ChangeState(PlayerStateType.Idle);
            }
        }
    }

    private void OnAnimationEnd()
    {
        _player.CanAttack = true;

        if (_player.CurrentComboCounter < 2)
            _keyDelay = 0.5f;
        else
            _keyDelay = 0.99f;

        _keyTimer = _keyDelay; //0.5초 기다리기 시작
    }

    public override void ExitState()
    {
        _player.AnimatorController.OnAnimationEndTrigger -= OnAnimationEnd;

        base.ExitState();
    }
}
