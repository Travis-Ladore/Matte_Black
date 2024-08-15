using UnityEngine;

public class SteeringController
{
    private readonly CarController _carController;

    private readonly CarControllerSettings _controllerSettings;
    private readonly SuspensionComponent _suspension;

    public SteeringController(CarController carController, CarControllerSettings controllerSettings, SuspensionComponent suspension)
    {
        _carController = carController;

        _controllerSettings = controllerSettings;
        _suspension = suspension;
    }

    public void Tick()
    {
        ApplySteering();
    }
    
    private void ApplySteering()
    {
        var targetSteeringAngle = _carController.InputController.TurnInput * _suspension.MaxSteeringAngle * _controllerSettings.SteeringCurve.Evaluate(_carController.CurrentSpeed);
        
        //drift steering code
        var forwardDirection = _carController.CarTransform.forward;
        targetSteeringAngle += _carController.DriftAxis * (Vector3.SignedAngle(forwardDirection, _carController.CarRigidbody.velocity + forwardDirection, Vector3.up));
        targetSteeringAngle = Mathf.Clamp(targetSteeringAngle, -_controllerSettings.MaxDriftSteeringAngle, _controllerSettings.MaxDriftSteeringAngle);
        
        for (var i = 0; i < 2; i++)
        {
            _carController.WheelColliders[i].steerAngle = Mathf.Lerp(_carController.WheelColliders[i].steerAngle, targetSteeringAngle, _controllerSettings.SteeringSpeed * Time.deltaTime);
        }
    }
}
