using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    #region ÄÄÆ÷³ÍÆ®
    private CharacterController _characterController;
    public CharacterController CharControllerCompo => _characterController;
    private Animator _animator;
    public Animator AnimatorCompo => _animator;
    #endregion

    private void Awake()
    {
        Transform visual = transform.Find("Visual").transform;
        _animator = visual.GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }
}
