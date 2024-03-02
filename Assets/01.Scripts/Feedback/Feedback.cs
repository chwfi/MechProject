using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    protected Entity _owner;

    protected virtual void Start()
    {
        _owner = GetComponentInParent<Entity>();
    }

    public abstract void CreateFeedback();
    public abstract void FinishFeedback();

    protected virtual void OnDestroy()
    {
        FinishFeedback();
    }

    protected virtual void OnDisable()
    {
        FinishFeedback();
    }
}
