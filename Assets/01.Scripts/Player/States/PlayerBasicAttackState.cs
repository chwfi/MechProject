using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerBasicAttackState : PlayerState
{
    private float _keyDelay = 0.35f;

    private int _currentCombo = 1; //현재 콤보가 몇인지 
    private bool _canAttack = true; // 선입력 막기 위해서 다음 공격가능상태인가
    private float _keyTimer = 0; //다음공격이 이뤄지기까지 기다리는 시간
    //이 시간내로 입력 안하면 idle로 돌아간다.
    private float _attackStartTime;

    public PlayerBasicAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("어태");

        _player.AnimatorController.OnAnimationEndTrigger += OnAnimationEnd;

        _player.AnimatorController.SetAttackCount(_player.CurrentComboCounter);
        _player.AnimatorController.SetAttackTrigger();
        _player.CurrentComboCounter++;

        _player.CanAttack = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_player.CanAttack && _keyTimer > 0)
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
        _keyTimer = _keyDelay; //0.5초 기다리기 시작
    }

    public override void ExitState()
    {
        _player.AnimatorController.OnAnimationEndTrigger -= OnAnimationEnd;

        base.ExitState();
    }
}
