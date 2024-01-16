using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;

    public event Action OnStartFireEvent;
    public event Action OnShootingFireEvent;
    public event Action OnStopFireEvent;

    public Vector3 MovementVector { get; private set; }
    public Vector2 AimPosition { get; private set; }
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
        MovementVector = context.ReadValue<Vector3>();
        //Vector3 value = context.ReadValue<Vector3>();
        //MovementEvent?.Invoke(value);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }
}
