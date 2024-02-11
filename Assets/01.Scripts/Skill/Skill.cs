using System;
using UnityEngine;

//public delegate void CooldownNotifier(float current, float total);

public class Skill : MonoBehaviour
{
    public bool skillEnable; //�� ��ų�� Ȱ��ȭ�Ǿ��°�?

    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected float _cooldown;
    protected float _cooldownTimer;
    protected Player _player;

    [SerializeField] protected PlayerSkill _skilltype;
    [SerializeField] protected int _collisionDetectCount; //����� �浹 �˻��Ҳ���.
    protected Collider2D[] _collisionColliders;

    public event Action<float, float> OnCooldown;

    //public event CooldownNotifier OnCooldown; //��Ÿ���� ���ư� �� ����Ǵ� �޼���

    protected virtual void Start()
    {
        _player = GameManager.Instance.Player;
        _collisionColliders = new Collider2D[_collisionDetectCount];
    }

    protected virtual void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
            }

            OnCooldown?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool AttemptUseSkill() //����ڰ� Ű�� ������ �� �ߵ��Ǵ� ��ų
    {
        if (_cooldownTimer <= 0 && skillEnable)
        {
            _cooldownTimer = _cooldown;
            UseSkill();
            return true;
        }

        Debug.Log("Skill cooldown or not unlocked!");
        return false;
    }

    public virtual void UseSkill() //Ư�� ȿ���� ���ؼ� �ߵ��Ǵ� ��ų (�������Ͼ���)
    {
        SkillManager.Instance.UseSkillFeedback(_skilltype);
    }

    public virtual Transform FindClosesetEnemy(Transform checkTrm, float radius)
    {
        Transform target = null;

        int cnt = Physics2D.OverlapCircle(checkTrm.position,
            radius, new ContactFilter2D
            { layerMask = _whatIsEnemy, useLayerMask = true }, _collisionColliders);

        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < cnt; i++)
        {
            Collider2D collider = _collisionColliders[i];
            float distance = Vector2.Distance(checkTrm.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = collider.transform;
            }
        }
        return target;
    }
}
