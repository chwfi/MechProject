using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAnimator : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("speed");

    private readonly int _walkHash = Animator.StringToHash("is_walk");
    private readonly int _runHash = Animator.StringToHash("is_run");

    private readonly int _attackCountHash = Animator.StringToHash("attack_count");
    private readonly int _attackHash = Animator.StringToHash("attack");

    private readonly int _dashAttackHash = Animator.StringToHash("is_dashAttack");

    public event Action OnAnimationEndTrigger = null;
    public event Action OnDashAttackEndTrigger = null;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void SetWalk(bool value)
    {
        _animator.SetBool(_walkHash, value);
    }

    public void SetRun(bool value)
    {
        _animator.SetBool(_runHash, value);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(_attackHash);
    }

    public void SetAttackCount(int value)
    {
        _animator.SetInteger(_attackCountHash, value);
    }

    public void SetDashAttack(bool value)
    {
        _animator.SetBool(_dashAttackHash, value);
    }

    #region Animation End Logics
    public void OnAnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }

    public void DashAttackEnd()
    {
        OnDashAttackEndTrigger?.Invoke();
    }
    #endregion
}
