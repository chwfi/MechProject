using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerBasicAttackState : PlayerState
{
    private float _keyDelay = 0.35f;

    private int _currentCombo = 1; //���� �޺��� ������ 
    private bool _canAttack = true; // ���Է� ���� ���ؼ� ���� ���ݰ��ɻ����ΰ�
    private float _keyTimer = 0; //���������� �̷�������� ��ٸ��� �ð�
    //�� �ð����� �Է� ���ϸ� idle�� ���ư���.
    private float _attackStartTime;

    public PlayerBasicAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("����");

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
        _keyTimer = _keyDelay; //0.5�� ��ٸ��� ����
    }

    public override void ExitState()
    {
        _player.AnimatorController.OnAnimationEndTrigger -= OnAnimationEnd;

        base.ExitState();
    }
}
