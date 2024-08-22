public class BrakeController
{
    private readonly CarController _carController;
    
    private readonly BrakeComponent _brakes;

    public BrakeController(CarController carController, BrakeComponent brakes)
    {
        _carController = carController;
        
        _brakes = brakes;
    }

    public void Tick()
    {
        ApplyBrake();
    }
    
    private void ApplyBrake()
    {
        //front wheel braking
        for (int i = 0; i < 2; i++)
        {
            _carController.WheelColliders[i].brakeTorque = _carController.InputController.BrakeInput * _brakes.BrakePower * _brakes.BrakeBias;
        }
        
        //rear wheel braking
        for (int i = 2; i < 4; i++)
        {
            _carController.WheelColliders[i].brakeTorque = _carController.InputController.BrakeInput * _brakes.BrakePower * (1 - _brakes.BrakeBias);
        }
    }
}
