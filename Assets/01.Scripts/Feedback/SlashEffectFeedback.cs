using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffectFeedback : Feedback
{
    [SerializeField] private EffectPlayer _effect;
    [SerializeField] private float _effectEndTime;

    private Player _player => _owner as Player;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_effect.name.ToString()) as EffectPlayer;
        effect.transform.position = _player.salshEffectPos.position;
        //effect.StartPlay(_effectEndTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
