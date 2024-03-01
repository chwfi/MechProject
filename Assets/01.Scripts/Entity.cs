using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public CharacterController CharacterControllerCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }

    //½ºÅÈ ³Ö±â
    protected StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    public bool IsDead => HealthCompo.currentHealth <= 0;

    public virtual void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
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
}
