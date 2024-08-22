using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;

    private Vector2 _lookInput;
    private float _driftCameraRotation;

    private CarControllerSettings _controllerSettings;

    [SerializeField] private float horizontalSensitivity;
    [SerializeField] private float verticalSensitivity;

    private void LateUpdate()
    {
        MoveCamera();
        DriftRotateCamera();
        
        //print(_driftCameraRotation);
    }

    public void SetUpComponents(CarControllerSettings controllerSettings)
    {
        _controllerSettings = controllerSettings;
    }
    public void SetLookInput(Vector2 lookInput)
    {
        _lookInput = lookInput;
    }
    public void ToggleRecenterCamera(bool shouldRecenter)
    {
        freeLookCamera.m_YAxisRecentering.m_enabled = shouldRecenter;
        freeLookCamera.m_RecenterToTargetHeading.m_enabled = shouldRecenter;
    }
    public void SetDriftCameraRotation(float driftCameraRotation)
    {
        _driftCameraRotation = driftCameraRotation;
    }
    
    private void MoveCamera()
    {
        freeLookCamera.m_XAxis.Value += -_lookInput.x * horizontalSensitivity;
        freeLookCamera.m_YAxis.Value += _lookInput.y * verticalSensitivity;
    }
    private void DriftRotateCamera()
    {
        freeLookCamera.m_Lens.Dutch =
            Mathf.Lerp(freeLookCamera.m_Lens.Dutch, _driftCameraRotation, Time.deltaTime * _controllerSettings.DriftCameraRotationSpeed);
    }
}
