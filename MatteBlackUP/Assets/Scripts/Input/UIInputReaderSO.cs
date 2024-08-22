using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Input Reader/Pause")]
public class UIInputReaderSO : InputReaderSO
{
    public Action onGamePaused;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        _controls.UI.Enable();
        
        _controls.UI.Pause.performed += Pause;
    }
    private void OnDisable()
    {
        _controls.UI.Disable();
        
        _controls.UI.Pause.performed -= Pause;
    }
    
    private void Pause(InputAction.CallbackContext context) => onGamePaused?.Invoke();
}