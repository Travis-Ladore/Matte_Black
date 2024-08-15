using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputData : MonoBehaviour
{
    public float ThrottleInput { get; private set; }
    public float BrakeInput { get; private set; }
    public float TurnInput { get; private set; }
    
    [SerializeField] private PlayerInputReaderSO _input;
    
    private void OnEnable()
    {
        _input.onThrottleChanged += Throttle;
        _input.onBrakeChanged += Brake;
        _input.onTurnChanged += Turn;
    }
    private void OnDisable()
    {
        _input.onThrottleChanged += Throttle;
        _input.onBrakeChanged += Brake;
        _input.onTurnChanged += Turn;
    }

    public void Throttle(InputAction.CallbackContext context) => ThrottleInput = context.ReadValue<float>();
    public void Brake(InputAction.CallbackContext context) => BrakeInput = context.ReadValue<float>();
    public void Turn(InputAction.CallbackContext context) => TurnInput = context.ReadValue<float>();
}
