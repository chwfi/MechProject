using System;
using UnityEngine;

public class Player : Entity
{
    [Range(0.1f, 1f)][SerializeField] private float _rotateSpeed;

    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    private Transform _visualTrm;

    public override void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
        CurrentHP = _maxHP;

        _visualTrm = transform.Find("Visual");

        CharacterControllerCompo = GetComponent<CharacterController>();
        AnimatorCompo = _visualTrm.GetComponent<Animator>();
        OnDead += OnDeadHandle;
    }

    public override void Start()
    {
        base.Start();
    }

    public void OnDisable()
    {
        _stateMachine.CurrentState.ExitState();
    }

    #region 움직임과 회전 관련
    public void SetVelocity(Vector3 dir)
    {
        CharacterControllerCompo.Move(dir);
    }

    public void Rotate(Vector3 dir, bool lerp = true)
    {
        var currentRotation = _visualTrm.rotation;
        var destRotation = Quaternion.LookRotation(dir);

        if (lerp)
        {
            _visualTrm.rotation = Quaternion.Lerp(currentRotation, destRotation, _rotateSpeed);
        }
        else
        {
            _visualTrm.rotation = destRotation;
        }
    }

    public void StopImmediately()
    {
        CharacterControllerCompo.Move(Vector3.zero);
    }
    #endregion

    protected override void RegisterStates()
    {
        foreach (PlayerStateType stateType in Enum.GetValues(typeof(PlayerStateType)))
        {
            var typeName = $"Player{stateType.ToString()}State";
            var type = Type.GetType(typeName);
            var state = Activator.CreateInstance(type, _stateMachine, this, stateType) as PlayerState;
            _stateMachine.RegisterState(stateType, state);
        }
    }

    protected override void SetInitState()
    {
        _stateMachine.Initialize(this, PlayerStateType.Idle);
    }


    public override void OnDamage(DamageType type, float damage)
    {
        base.OnDamage(type, damage);
        //SoundManager.Instance.PlaySFX("PlayerHit");
    }

    public void SetAnimationSpeed(float speed)
    {
        AnimatorCompo.speed = speed;
    }

    public void ResetAnimationSpeed()
    {
        AnimatorCompo.speed = 1f;
    }

    private void OnDeadHandle()
    {
        _stateMachine.CurrentState.ExitState();
    }
}
