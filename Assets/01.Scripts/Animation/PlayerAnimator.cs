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

    public event Action OnAnimationEndTrigger = null;

    private Animator _animator;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = transform.parent.GetComponent<Player>();
    }

    public void OnAnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
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
}
