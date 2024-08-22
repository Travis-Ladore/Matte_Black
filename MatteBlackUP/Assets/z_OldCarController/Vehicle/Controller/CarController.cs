using UnityEngine;

public class CarController
{
    private readonly CarManager _carManager;

    #region Components

    private readonly CarControllerSettings _controllerSettings;
    private readonly CarSpecComponent _carSpec;
    private readonly EngineComponent _engine;
    private readonly BrakeComponent _brakes;
    private readonly GearboxComponent _gearbox;
    private readonly SuspensionComponent _suspension;
    private readonly TireComponent _tires;
    private readonly AeroComponent _aero;

    #endregion

    #region Controllers

    private CarAudioController _carAudioController;
    private CameraController _cameraController;
    private VfxController _vfxController;
    
    private CarController _carController;
    private ThrottleController _throttleController;
    private BrakeController _brakeController;
    private GearboxController _gearboxController;
    private SteeringController _steeringController;
    private DriftController _driftController;
    private AeroController _downforceController;
    
    #endregion

    #region References

    public WheelCollider[] WheelColliders = new WheelCollider[4];
    private Transform[] _wheelTransforms = new Transform[4];
    
    public Rigidbody CarRigidbody;
    
    private CarSpeedMeter _speedMeter;

    public readonly Transform CarTransform;

    #endregion

    #region Input

    public readonly PlayerInputController InputController;

    #endregion

    #region Engine

    public float Rpm;
    public float WheelRpm;
    
    public float CurrentTorque;

    #endregion

    #region Gearbox

    public float Clutch;
    
    public int CurrentGear;
    public GearState CurrentGearState = GearState.Running;

    #endregion

    #region Tires
    
    private float _wheelRadius;
    
    public readonly float[] TireSlip = new float[4];

    #endregion

    #region Drift

    public float DriftAxis;

    public float SlipAngle;

    #endregion

    public float CurrentSpeed;

    public CarController(Transform transform, CarManager carManager, CarControllerSettings controllerSettings, PlayerInputController inputController, 
        CarSpecComponent carSpec, EngineComponent engine, BrakeComponent brakes, GearboxComponent gearbox, 
        SuspensionComponent suspension, TireComponent tires, AeroComponent aero)
    {
        CarTransform = transform;
        _carManager = carManager;
        _controllerSettings = controllerSettings;
        InputController = inputController;

        _carSpec = carSpec;
        _engine = engine;
        _brakes = brakes;
        _gearbox = gearbox;
        _suspension = suspension;
        _tires = tires;
        _aero = aero;

    }
    
    public void InitializeDependencies(GameObject camera, CarSpeedMeter carSpeedMeter, WheelCollider[] wheelColliders, Transform[] wheelTransforms, GameObject tireSmokeParticle, GameObject tireSkidMarks)
    {
        SetUpCarSpecs();
        SetUpUI(carSpeedMeter);
        SetUpWheelColliders(wheelColliders, wheelTransforms);
        
        _wheelRadius = WheelColliders[2].radius;

        _carAudioController = new CarAudioController(this, _controllerSettings, _engine, _gearbox);
        _vfxController = new VfxController(this, _controllerSettings, tireSmokeParticle, tireSkidMarks);
        _cameraController = new CameraController(this, _controllerSettings, camera);
        _cameraController.SpawnCamera();
        
        _throttleController = new ThrottleController(this, _controllerSettings,_engine, _gearbox, _carSpec, _wheelRadius);
        _brakeController = new BrakeController(this, _brakes);
        _gearboxController = new GearboxController(this, _gearbox);
        _steeringController = new SteeringController(this, _controllerSettings, _suspension);
        _driftController = new DriftController(this, _controllerSettings, _carSpec);
        _downforceController = new AeroController(this, _aero);

    }

    private void SetUpUI(CarSpeedMeter carSpeedMeter)
    {
        _speedMeter = carSpeedMeter;
        _speedMeter.SetUpRevMeter(_gearbox.UpShiftRpm, _gearbox.RedLineRpm);
    }
    
    public void Tick()
    {
        _throttleController.Tick();
        _brakeController.Tick();
        _gearboxController.Tick();
        _steeringController.Tick();
        _driftController.Tick();
        _downforceController.Tick();
        
        _carAudioController.Tick();
        _vfxController.Tick();
        _cameraController.Tick();
        
        UpdateWheelMeshes();
        CalculateCurrentSpeed();
        CalculateTireSlip();
        UpdateSpeedometer();
        
        CalculateSlipDirection();
    }

    private void UpdateSpeedometer()
    {
        _speedMeter.UpdateRevMeter(Rpm);
        _speedMeter.UpdateSpeedText(CurrentSpeed);
        
        var currentGear = CurrentGearState == GearState.ChangingGear ? "N" : (CurrentGear + 1).ToString();
        _speedMeter.UpdateGearText(currentGear);
    }

    private void SetUpCarSpecs()
    {
        CarRigidbody = _carManager.GetComponent<Rigidbody>();
        
        CarRigidbody.mass = _carSpec.Mass;

        var transforms = _carManager.GetComponentsInChildren<Transform>();
        foreach (var transform in transforms)
        {
            if (!transform.CompareTag("CenterOfMass")) continue;
            CarRigidbody.centerOfMass = transform.localPosition;
            break;
        }
    }

    private void CalculateCurrentSpeed()
    {
        CurrentSpeed = CarRigidbody.velocity.magnitude * 3.6f;
    }

    private void UpdateWheelMeshes()
    {
        for (var i = 0; i < 4; i++)
        {
            WheelColliders[i].GetWorldPose(out Vector3 targetWheelPosition, out Quaternion targetWheelRotation);

            _wheelTransforms[i].position = targetWheelPosition;
            _wheelTransforms[i].rotation = targetWheelRotation;
        }
    }
    private void SetUpWheelColliders(WheelCollider[] wheelColliders, Transform[] wheelTransforms)
    {
        WheelColliders = wheelColliders;
        _wheelTransforms = wheelTransforms;
    }

    private void CalculateTireSlip()
    {
        for (int i = 0; i < TireSlip.Length; i++)
        {
            WheelColliders[i].GetGroundHit(out var wheelHit);

            var wheelSlip = Mathf.Abs(wheelHit.sidewaysSlip) + Mathf.Abs(wheelHit.forwardSlip);

            TireSlip[i] = wheelSlip;
        }
    }

    private void CalculateSlipDirection()
    {
        var targetRotation = Quaternion.LookRotation(CarRigidbody.velocity, Vector3.up);

        _carManager.UpdateArrowDirection(targetRotation);
    }
}
