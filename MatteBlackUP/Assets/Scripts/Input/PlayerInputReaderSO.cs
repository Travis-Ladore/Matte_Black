using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reader/Player")]
public class PlayerInputReaderSO : InputReaderSO
{
    public Action<InputAction.CallbackContext> onThrottleChanged;
    public Action<InputAction.CallbackContext> onBrakeChanged;
    public Action<InputAction.CallbackContext> onTurnChanged;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        _controls.Default.Enable();
        
        _controls.Default.Throttle.performed += Throttle;
        _controls.Default.Throttle.canceled += Throttle;
        
        _controls.Default.Brake.performed += Brake;
        _controls.Default.Brake.canceled += Brake;
        
        _controls.Default.Turn.performed += Turn;
        _controls.Default.Turn.canceled += Turn;
    }
    private void OnDisable()
    {
        _controls.Default.Disable();
        
        _controls.Default.Throttle.performed -= Throttle;
        _controls.Default.Throttle.canceled -= Throttle;
        
        _controls.Default.Brake.performed -= Brake;
        _controls.Default.Brake.canceled -= Brake;
        
        _controls.Default.Turn.performed -= Turn;
        _controls.Default.Turn.canceled -= Turn;
    }

    private void Throttle(InputAction.CallbackContext context) => onThrottleChanged?.Invoke(context);
    private void Brake(InputAction.CallbackContext context) => onBrakeChanged?.Invoke(context);
    private void Turn(InputAction.CallbackContext context) => onTurnChanged?.Invoke(context);
}