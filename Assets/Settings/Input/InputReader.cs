using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action BasicAttackEvent;

    public bool IsShiftPressed;

    public Vector3 MovementInput { get; private set; }
    public Vector3 AimPosition { get; private set; }
    private Controls _playerInputAction; 

    private void OnEnable()
    {
        if (_playerInputAction == null)
        {
            _playerInputAction = new Controls();
            _playerInputAction.Player.SetCallbacks(this); 
        }

        _playerInputAction.Player.Enable(); 
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        MovementInput = new Vector3(input.x, 0, input.y);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        IsShiftPressed = context.performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            BasicAttackEvent?.Invoke();
    }
}
