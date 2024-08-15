using UnityEngine;

public class DriftController
{
    private readonly CarController _carController;
    
    private readonly CarControllerSettings _controllerSettings;
    private readonly CarSpecComponent _carSpec;

    public DriftController(CarController carController, CarControllerSettings controllerSettings, CarSpecComponent carSpec)
    {
        _carController = carController;
        _controllerSettings = controllerSettings;

        _carSpec = carSpec;
    }

    public void Tick()
    {
        SetSlipAngle();
        SetDriftAxis();
        DriftRotation();
    }

    private void SetSlipAngle()
    {
        var forwardDirection = _carController.CarTransform.forward;
        var velocityDirection = _carController.CarRigidbody.velocity + forwardDirection;
        
        _carController.SlipAngle = Vector3.SignedAngle(forwardDirection, velocityDirection, Vector3.up);
    }

    private void SetDriftAxis()
    {
        var driftDelta = _carController.SlipAngle / _controllerSettings.DriftStartAngle;
        driftDelta = Mathf.Clamp(driftDelta, -1, 1);
        driftDelta = Mathf.Abs(driftDelta);

        _carController.DriftAxis = Mathf.Lerp(0f, 1f, driftDelta);
    }

    private void DriftRotation()
    {
        Vector3 rotationDirection = _carController.CarTransform.up * (_carSpec.Mass * _controllerSettings.DriftTorqueMultiplier);

        var driftRotationDelta = Mathf.Abs(_carController.SlipAngle);
        driftRotationDelta = Remap(driftRotationDelta, 0f, _controllerSettings.MaxDriftAngle, 0f, 1f);
        driftRotationDelta = 1f - driftRotationDelta;

        _carController.CarRigidbody.AddTorque(rotationDirection * (_carController.InputController.DriftInput * driftRotationDelta));
    }
    
    private static float Remap (float value, float inputMin, float inputMax, float outputMin, float outputMax) {
        var remappedValue = (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
        remappedValue = Mathf.Clamp(remappedValue, outputMin, outputMax);
        return remappedValue;
    }
}
