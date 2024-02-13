using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Range(0.1f, 1f)]
    [SerializeField] private float _rotateSpeed;

    [Header("Attack ����")]
    [SerializeField] private float _attackDelayTime = 0.75f;
    public float AttackDelayTime { get { return _attackDelayTime; } set { _attackDelayTime = value; } }
    [HideInInspector] public float AttackTimer = 0;
    [SerializeField] private float _attackMoveTime;
    public float AttackMoveTime => _attackMoveTime;
    [SerializeField] private float _attackMoveDelay;
    public float AttackMoveDelay => _attackMoveDelay;
    [SerializeField] private float _attackMoveSpeed;
    public float AttackMoveSpeed => _attackMoveSpeed;
    public int CurrentComboCounter = 0;
    public bool CanAttack = true;

    [Header("Dash ����")]
    [SerializeField] private float _dashTime;
    public float DashTime => _dashTime;
    [SerializeField] private float _dashDelay;
    public float DashDelay => _dashDelay;
    [SerializeField] private float _dashSpeed;
    public float DashSpeed => _dashSpeed;

    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    public PlayerAnimator AnimatorController { get; set; }

    private Transform _visualTrm;

    public override void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
        CurrentHP = _maxHP;

        _visualTrm = transform.Find("Visual");

        CharacterControllerCompo = GetComponent<CharacterController>();
        AnimatorCompo = _visualTrm.GetComponent<Animator>();
        AnimatorController = _visualTrm.GetComponent<PlayerAnimator>();

        OnDead += OnDeadHandle;
    }

    public override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _stateMachine.CurrentState.ExitState();
    }

    #region �����Ӱ� ȸ�� ����
    public void SetVelocity(Vector3 dir)
    {
        CharacterControllerCompo.Move(dir);
    }

    public void Dash(Vector3 dir, float delay, float time, float speed)
    {
        StopCoroutine(DashCoroutine(dir, delay, time, speed));
        StartCoroutine(DashCoroutine(dir, delay, time, speed));
    }

    private IEnumerator DashCoroutine(Vector3 dir, float delay, float time, float speed)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        while (Time.time < startTime + time)
        {
            CharacterControllerCompo.Move(dir * speed * Time.deltaTime);

            yield return null;
        }
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

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

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
