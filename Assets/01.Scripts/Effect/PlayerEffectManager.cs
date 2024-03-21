using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private EffectPlayer[] _bladeEffects;

    private void Awake()
    {
        _bladeEffects = transform.Find("BladeEffects").GetComponentsInChildren<EffectPlayer>();
    }

    public void BladeEffect()
    {
        _bladeEffects[0].StartPlay(0.5f);
    }
}
