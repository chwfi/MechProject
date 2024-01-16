using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    #region Components
    private CharacterController _characterController;
    public CharacterController CharControllerCompo => _characterController;
    private Animator _animator;
    public Animator AnimatorCompo => _animator;
    #endregion

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;

    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual").transform;
        _animator = visualTrm.GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SetMovement();
    }

    public void SetMovement()
    {
        Vector3 move = new Vector3(_inputReader.MovementVector.x, 0, _inputReader.MovementVector.z).normalized;
        _characterController.Move(move * 5 * Time.deltaTime);
    }
}
