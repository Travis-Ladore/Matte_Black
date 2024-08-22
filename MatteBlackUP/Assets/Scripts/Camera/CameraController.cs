using UnityEngine;

public class CameraController
{
    private readonly CarController _carController;
    private readonly CarControllerSettings _controllerSettings;
    private readonly GameObject _spawnCamera;

    private Camera _camera;

    public CameraController(CarController carController, CarControllerSettings controllerSettings, GameObject cameraPrefab)
    {
        _carController = carController;
        _controllerSettings = controllerSettings;
        _spawnCamera = cameraPrefab;
    }
    public void SpawnCamera()
    {
        var spawnTransform = _carController.CarTransform;
        
        _camera = Object.Instantiate(_spawnCamera, spawnTransform).GetComponent<Camera>();
        _camera.transform.parent = _carController.CarTransform;
        _camera.SetUpComponents(_controllerSettings);
    }

    public void Tick()
    {
        MoveCamera();
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (Mathf.Abs(_carController.SlipAngle) < 20f || _carController.InputController.FreeLookInput)
        {
            _camera.SetDriftCameraRotation(0f);
            return;
        }
        
        var driftCameraRotation = Remap(_carController.SlipAngle, -_controllerSettings.MaxDriftAngle, _controllerSettings.MaxDriftAngle, -_controllerSettings.MaxDriftCameraRotation, _controllerSettings.MaxDriftCameraRotation);
        _camera.SetDriftCameraRotation(driftCameraRotation);
    }
    private void MoveCamera()
    {
        if (!_carController.InputController.FreeLookInput)
        {
            _camera.ToggleRecenterCamera(true);
            return;
        }
        
        _camera.ToggleRecenterCamera(false);
        _camera.SetLookInput(_carController.InputController.CameraLookInput);
    }
    
    private static float Remap (float value, float inputMin, float inputMax, float outputMin, float outputMax) {
        var remappedValue = (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
        remappedValue = Mathf.Clamp(remappedValue, outputMin, outputMax);
        return remappedValue;
    }
}
