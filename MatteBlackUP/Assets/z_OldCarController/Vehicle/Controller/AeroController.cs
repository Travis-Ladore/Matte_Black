public class AeroController
{
    private readonly CarController _carController;

    private readonly AeroComponent _aero;

    public AeroController(CarController carController, AeroComponent aero)
    {
        _carController = carController;
        
        _aero = aero;
    }

    public void Tick()
    {
        LaunchDownforce();
    }

    private void LaunchDownforce()
    {
        var forceDirection = -_carController.CarTransform.up;

        var downforceDelta = Remap(_carController.CurrentSpeed, 30f, 100f, 0f, 1f);
        
        _carController.CarRigidbody.AddForce(forceDirection * (_aero.MaxDownforce * downforceDelta));
    }

    private static float Remap (float value, float inputMin, float inputMax, float outputMin, float outputMax) {
        var remappedValue = (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
        return remappedValue;
    }
}
