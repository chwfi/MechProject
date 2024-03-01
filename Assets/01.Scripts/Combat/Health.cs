using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;

    public bool IsDead = false;

    #region components
    private BaseStat _owner;
    #endregion

    #region events
    public Action OnHit;
    public Action OnDead;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;
    #endregion

    public void SetHealth(BaseStat owner)
    {
        _owner = owner;

        maxHealth = owner.maxHealth.GetValue();
        currentHealth = maxHealth;
    }

    public void OnDamage(DamageType type, int damage)
    {
        if (IsDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0)
        {
            IsDead = true;
            OnDead?.Invoke();
            OnDeadEvent?.Invoke();
        }
    }
}
