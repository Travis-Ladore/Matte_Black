using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThrottleController
{
    private readonly CarController _carController;

    private readonly CarControllerSettings _controllerSettings;
    private readonly EngineComponent _engine;
    private readonly GearboxComponent _gearbox;
    private readonly CarSpecComponent _carSpec;
    
    private readonly float _wheelRadius;
    private float _engineRpm;

    public ThrottleController(CarController carController, CarControllerSettings controllerSettings, EngineComponent engine, GearboxComponent gearbox, CarSpecComponent carSpec, float wheelRadius)
    {
        _carController = carController;

        _controllerSettings = controllerSettings;
        _engine = engine;
        _gearbox = gearbox;
        _carSpec = carSpec;

        _wheelRadius = wheelRadius;
    }

    public void Tick()
    {
        ApplyThrottle();
    }
    
    private void ApplyThrottle()
    {
        //slipAngle = Vector3.Angle(transform.forward, carRigidbody.velocity - transform.forward);

        _carController.CurrentTorque = CalculateTorque();
        
        var targetMotorTorque = (_carController.CurrentTorque * _carController.InputController.ThrottleInput) * _controllerSettings.DriftPowerMultiplier.Evaluate(_carController.DriftAxis);

        switch (_carSpec.DriveTrain)
        {
            case Drivetrain.AllWheelDrive:
                for (int i = 0; i < 4; i++)
                {
                    _carController.WheelColliders[i].motorTorque = targetMotorTorque / 4;
                }
                break;
            
            case Drivetrain.FrontWheelDrive:
                for (int i = 0; i < 2; i++)
                {
                    _carController.WheelColliders[i].motorTorque = targetMotorTorque / 2;
                }
                break;
            
            case Drivetrain.RearWheelDrive:
                for (int i = 2; i < 4; i++)
                {
                    _carController.WheelColliders[i].motorTorque = targetMotorTorque / 2;
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private float CalculateTorque()
    {
        var torque = 0f;
        if (_carController.Clutch < 0.1f)
        {
            _carController.Rpm = Mathf.Lerp(_carController.Rpm, Mathf.Max(_gearbox.IdleRpm, _gearbox.RedLineRpm * _carController.InputController.ThrottleInput) + Random.Range(-100, 100), Time.deltaTime);
            _engineRpm = Mathf.Lerp(_engineRpm, Mathf.Max(_gearbox.IdleRpm, _gearbox.RedLineRpm * _carController.InputController.ThrottleInput) + Random.Range(-100, 100), Time.deltaTime);
        }
        else
        {
            var wheelRpm = GetDriveWheelRpm();

            var currentWheelRpm = ((_carController.CurrentSpeed * 5) / 18) / (_wheelRadius / 60f);
            _carController.WheelRpm = currentWheelRpm;
            
            var targetEngineRpm = wheelRpm * _gearbox.GearRatios[_carController.CurrentGear];
            _engineRpm = Mathf.Lerp(_engineRpm, Mathf.Max(_gearbox.IdleRpm, targetEngineRpm), Time.deltaTime * 3f);

            var targetRpm = currentWheelRpm * _gearbox.GearRatios[_carController.CurrentGear];
            _carController.Rpm = Mathf.Lerp(_carController.Rpm, Mathf.Max(_gearbox.IdleRpm, targetRpm), Time.deltaTime * 3f);
            
            torque = ((_engine.PowerCurve.Evaluate(_engineRpm / _gearbox.RedLineRpm) * _engine.MaxHorsePower) /
                     (_engineRpm / 7127)) * _carController.Clutch;
        }
        
        return torque;
    }
    
    private float GetDriveWheelRpm()
    {
        int driveWheels;
        var totalRpm = 0f;
        
        switch (_carSpec.DriveTrain)
        {
            case Drivetrain.AllWheelDrive:
                for (var i = 0; i < 4; i++)
                {
                    totalRpm += _carController.WheelColliders[i].rpm;
                }
                driveWheels = 4;
                break;
            case Drivetrain.FrontWheelDrive:
                for (var i = 0; i < 2; i++)
                {
                    totalRpm += _carController.WheelColliders[i].rpm;
                }
                driveWheels = 2;
                break;
            case Drivetrain.RearWheelDrive:
                for (var i = 2; i < 4; i++)
                {
                    totalRpm += _carController.WheelColliders[i].rpm;
                }
                driveWheels = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return totalRpm / driveWheels;
    }
}
