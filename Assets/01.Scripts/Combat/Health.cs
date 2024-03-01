using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;

    #region components
    private Entity _owner;
    #endregion

    #region property
    #endregion

    public void SetHealth(Entity owner)
    {
        _owner = owner;
    }

    public void OnDamage(DamageType type, float damage)
    {
        throw new System.NotImplementedException();
    }
}
