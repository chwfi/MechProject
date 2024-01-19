using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public CharacterController CharacterControllerCompo { get; protected set; }
    public Animator AnimatorCompo { get; protected set; }

    //스탯 넣기
    protected StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    public float MoveSpeed; //임시 나중에 SO로 뺄 것

    [SerializeField] protected float _maxHP; //임시 나중에 Stat SO로 할 것
    public float CurrentHP { get; protected set; } //임시
    public bool IsDead => CurrentHP <= 0;

    protected readonly int _deadHash = Animator.StringToHash("Dead");

    public event Action OnDamaged;
    public event Action OnDead;

    public virtual void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
        CurrentHP = _maxHP;
    }

    public virtual void Start()
    {
        SetInitState();
    }

    public virtual void Update()
    {
        if (!IsDead)
            _stateMachine.CurrentState?.UpdateState();
    }

    protected abstract void RegisterStates();
    protected abstract void SetInitState();

    public virtual void OnDamage(DamageType type, float damage)
    {
        if (IsDead) 
            return;

        CurrentHP -= damage;
        OnDamaged?.Invoke();

        CurrentHP = Mathf.Clamp(CurrentHP, 0, _maxHP);

        if (CurrentHP <= 0)
        {
            AnimatorCompo.SetTrigger(_deadHash);
            OnDead?.Invoke();
        }
    }
}
