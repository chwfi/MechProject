using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class EffectPlayer : PoolableMono 
{
    [SerializeField]
    private List<ParticleSystem> _particles;
    public List<ParticleSystem> Particles => _particles;

    public void StartPlay(float delayTime)
    {
        float time;

        if (Time.time > delayTime)
        {
            if (_particles != null)
                _particles.ForEach(p => p.Play());
        }
    }
}