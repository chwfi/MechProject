using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    [SerializeField] private PlayerStat _playerStat;
    public PlayerStat PlayerStat => _playerStat;

    #region setting values
    [Header("이동 회전 관련")]
    public float MoveSpeed;
    [Range(0.1f, 1f)]
    [SerializeField] private float _rotateSpeed;

    [Header("Attack 관련")]
    [SerializeField] private float _attackDelayTime = 0.75f;
    [HideInInspector] public float AttackTimer = 0;
    [SerializeField] private float _attackMoveTime;
    [SerializeField] private float _attackMoveDelay;
    [SerializeField] private float _attackMoveSpeed;
    public Transform salshEffectPos;
    public int CurrentComboCounter = 0;
    public bool CanAttack = true;

    [Header("Dash 관련")]
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashSpeed;
    #endregion

    #region property
    public float AttackDelayTime { get { return _attackDelayTime; } set { _attackDelayTime = value; } }
    public float AttackMoveTime => _attackMoveTime;
    public float AttackMoveDelay => _attackMoveDelay;
    public float AttackMoveSpeed => _attackMoveSpeed;
    public float DashTime => _dashTime;
    public float DashDelay => _dashDelay;
    public float DashSpeed => _dashSpeed;
    #endregion

    #region components
    public PlayerAnimator AnimatorController { get; set; }
    public PlayerEffectManager PlayerEffectManager { get; set; }
    public List<FeedbackPlayer> PlayerFeedbacks { get; set; }
    private Transform _visualTrm;
    #endregion

    public override void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();

        _visualTrm = transform.Find("Visual");

        CharacterControllerCompo = GetComponent<CharacterController>();
        HealthCompo = GetComponent<Health>();
        AnimatorController = _visualTrm.GetComponent<PlayerAnimator>();
        PlayerEffectManager = transform.Find("VFX").GetComponent<PlayerEffectManager>();
        //PlayerFeedbacks = transform.Find("PlayerFeedbacks").GetComponentsInChildren<FeedbackPlayer>().ToList();
    }

    public override void Start()
    {
        base.Start();

        MoveSpeed = _playerStat.moveSpeed.GetValue();

        HealthCompo.SetHealth(_playerStat);
    }

    private void OnDisable()
    {
        _stateMachine.CurrentState.ExitState();
    }

    #region 움직임과 회전 관련
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

    public void PlayFeedback(string feedbackName)
    {
        FeedbackPlayer feedbacks = PlayerFeedbacks.Find(feedback => feedback.name == feedbackName);

        if (feedbacks != null)
        {
            feedbacks.PlayFeedback();
        }
    }

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

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
