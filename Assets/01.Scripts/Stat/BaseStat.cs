using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum StatType
{
    maxHealth,
    moveSpeed,
    damage,
    criticalChance,
    criticalDamage,
}

public abstract class BaseStat : ScriptableObject
{
    [Header("Default stats")]
    public Stat maxHealth;
    public Stat moveSpeed;
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalDamage;

    protected Entity _owner;

    protected Dictionary<StatType, FieldInfo> _filedInfoDictionary = new Dictionary<StatType, FieldInfo>();

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public virtual void IncreaseStatBy(int modifyValue, float duration, Stat statToModify)
    {
        _owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, statToModify));
    }

    private IEnumerator StatModifyCoroutine(int modifyValue, float duration, Stat statToModify)
    {
        statToModify.AddModifier(modifyValue);
        yield return new WaitForSeconds(duration);
        statToModify.RemoveModifier(modifyValue);
    }

    protected virtual void OnEnable()
    {

    }

    public int GetDamage()
    {
        return damage.GetValue();
    }
}
