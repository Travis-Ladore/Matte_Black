using System.Linq;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    #region Controller Settings

    [Header("Settings")]
    [Space(1)] 
    [SerializeField] private CarControllerSettings controllerSettings;
    [Space(10)]

    #endregion

    #region Spawn Components

    [Header("Spawn Objects")] 
    [Space(1)] 
    [SerializeField] private GameObject uiSpeedometer;
    [Space(1)] 
    [SerializeField] private GameObject followCamera;
    [Space(1)] 
    [SerializeField] private GameObject tireSmokeParticles;
    [Space(1)] 
    [SerializeField] private GameObject tireSkidMarks;
    [Space(10)]

    #endregion

    #region Car Components

    [Header("Car Components")]
    [Space(5)]
    [SerializeField] private CarSpecComponent carSpec;
    [Space(5)] 
    [SerializeField] private EngineComponent engine;
    [Space(5)]
    [SerializeField] private BrakeComponent brakes;
    [Space(5)] 
    [SerializeField] private GearboxComponent gearbox;
    [Space(5)] 
    [SerializeField] private SuspensionComponent suspension;
    [Space(5)] 
    [SerializeField] private TireComponent tires;
    [Space(5)] 
    [SerializeField] private AeroComponent aero;
    [Space(10)]

    #endregion

    #region Controllers

    #region Controllers

    private CarController _carController;
    
    private PlayerInputController _inputControllerController;

    #endregion

    #endregion

    [SerializeField] private Transform arrowTransform;

    private void OnEnable()
    {
        _inputControllerController = GetComponent<PlayerInputController>();
        
        //initialize all controller scripts
        _carController = new CarController(transform, this, controllerSettings, _inputControllerController, carSpec, engine, brakes, 
            gearbox, suspension, tires, aero);
        
        var wheelTransforms = GetWheelMeshTransforms();
        var wheelColliders = GetComponentsInChildren<WheelCollider>();

        var canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
        var speedometer = Instantiate(uiSpeedometer, canvasTransform).GetComponent<CarSpeedMeter>();
        
        _carController.InitializeDependencies(followCamera, speedometer, wheelColliders, wheelTransforms, tireSmokeParticles, tireSkidMarks);
    }

    private void FixedUpdate()
    {
        _carController.Tick();
    }

    private Transform[] GetWheelMeshTransforms()
    {
        var allMeshes = GetComponentsInChildren<MeshRenderer>();

        var wheelMeshes = (from mesh in allMeshes where mesh.CompareTag("Wheels") select mesh.GetComponent<Transform>()).ToArray();
        return wheelMeshes;
    }

    public void UpdateArrowDirection(Quaternion newRotation)
    {
        arrowTransform.rotation = newRotation;
    }
}
