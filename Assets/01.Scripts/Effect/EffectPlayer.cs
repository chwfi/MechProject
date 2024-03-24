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
        StartCoroutine(Play(delayTime));
    }

    private IEnumerator Play(float time)
    {
        yield return new WaitForSeconds(time);

        if (_particles != null)
            _particles.ForEach(p => p.Play());
    }
}