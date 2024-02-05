using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("speed");

    private readonly int _walkHash = Animator.StringToHash("is_walk");
    private readonly int _runHash = Animator.StringToHash("is_run");

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
}
