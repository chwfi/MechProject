using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/PlayerStat")]
public class PlayerStat : BaseStat
{
    protected override void OnEnable()
    {
        base.OnEnable();

        Type playerStatType = typeof(PlayerStat);

        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            FieldInfo playerStatField = playerStatType.GetField(statType.ToString());
            if (playerStatField == null)
            {
                Debug.LogError($"There are no stat! error : {statType.ToString()}");
            }
            else
            {
                _filedInfoDictionary.Add(statType, playerStatField);
            }
        }
    }

    public Stat GetStatByType(StatType statType)
    {
        return _filedInfoDictionary[statType].GetValue(this) as Stat;
    }
}
