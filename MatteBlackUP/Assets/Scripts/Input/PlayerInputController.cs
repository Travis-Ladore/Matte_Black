using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public float ThrottleInput { get; private set; }
    public float BrakeInput { get; private set; }
    public float TurnInput { get; private set; }
    public float DriftInput { get; private set; }
    public bool FreeLookInput { get; private set; }
    public Vector2 CameraLookInput { get; private set; }

    public void Throttle(InputAction.CallbackContext context)
    {
        ThrottleInput = context.ReadValue<float>();
        ThrottleInput = Mathf.Clamp01(ThrottleInput);
    }

    public void Brake(InputAction.CallbackContext context)
    {
        BrakeInput = context.ReadValue<float>();
    }

    public void Turn(InputAction.CallbackContext context)
    {
        TurnInput = context.ReadValue<float>();
    }

    public void Drift(InputAction.CallbackContext context)
    {
        if (FreeLookInput)
        {
            DriftInput = 0f;
            return;
        }
        
        DriftInput = context.ReadValue<float>();
    }

    public void FreeLook(InputAction.CallbackContext context)
    {
        FreeLookInput = context.ReadValueAsButton();
    }

    public void CameraLook(InputAction.CallbackContext context)
    {
        CameraLookInput = context.ReadValue<Vector2>();
    }
    
}
