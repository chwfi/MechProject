using System;
using UnityEngine;

public class PlayerState : State //�÷��̾�� ���õ� State���� �θ�. �÷��̾� State���� ����Ǵ� �ൿ���� �����Ѵ�.
{
    protected Player _player => _owner as Player;

    public PlayerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        _player.AnimatorController.OnAnimationEndTrigger += OnAnimationEnd;
        _player.InputReader.DashAttackEvent += OnDashAttack;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();

        _player.AnimatorController.OnAnimationEndTrigger -= OnAnimationEnd;
        _player.InputReader.DashAttackEvent -= OnDashAttack;
    }

    private void OnDashAttack()
    {
        Skill skill = SkillManager.Instance.GetSkill<DashAttackSkill>();

        if (skill != null && skill.AttemptUseSkill() && _player.CanAttack)
        {
            _stateMachine.ChangeState(PlayerStateType.DashAttack);
        }
    }

    private void OnAnimationEnd()
    {
        _player.CanAttack = true;
        _player.AttackTimer = 0;
    }
}