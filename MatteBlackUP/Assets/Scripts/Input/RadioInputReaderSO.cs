using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reader/Radio")]
public class RadioInputReaderSO : InputReaderSO
{
    public Action<InputAction.CallbackContext> onTrackChanged;
    public Action<InputAction.CallbackContext> onDiscChanged;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        _controls.Radio.Enable();
        
        _controls.Radio.Track.performed += Track;
        _controls.Radio.Disc.performed += Disc;
    }
    private void OnDisable()
    {
        _controls.Radio.Disable();
        
        _controls.Radio.Track.performed -= Track;
        _controls.Radio.Disc.performed -= Disc;
    }

    private void Track(InputAction.CallbackContext context) => onTrackChanged?.Invoke(context);

    private void Disc(InputAction.CallbackContext context) => onDiscChanged?.Invoke(context);
}