using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarController/CarControllerSettings", order = 1)]
public class CarControllerSettings : ScriptableObject
{
    [SerializeField] private float steeringSpeed;
    [SerializeField] private AnimationCurve steeringCurve;
    
    [SerializeField] private AnimationCurve driftPowerMultiplier;
    [SerializeField] private float driftStartAngle;
    [SerializeField] private float driftTorqueMultiplier;
    [SerializeField] private float maxDriftSteeringAngle;
    [SerializeField] private float maxDriftAngle;
    [SerializeField] private float maxDriftCameraRotation;
    [SerializeField] private float driftCameraRotationSpeed;

    [SerializeField] private float smokeThreshold;
    [SerializeField] private float skidThreshold;

    [SerializeField] private AudioClip tireSkidAudio;

    public float SteeringSpeed
    {
        get { return steeringSpeed; }
    }
    
    public AnimationCurve SteeringCurve
    {
        get { return steeringCurve; }
    }
    
    public AnimationCurve DriftPowerMultiplier
    {
        get { return driftPowerMultiplier; }
    }

    public float DriftStartAngle
    {
        get { return driftStartAngle; }
    }

    public float DriftTorqueMultiplier
    {
        get { return driftTorqueMultiplier; }
    }

    public float MaxDriftSteeringAngle
    {
        get { return maxDriftSteeringAngle; }
    }

    public float MaxDriftAngle
    {
        get { return maxDriftAngle; }
    }

    public float MaxDriftCameraRotation
    {
        get { return maxDriftCameraRotation; }
    }

    public float DriftCameraRotationSpeed
    {
        get { return driftCameraRotationSpeed; }
    }

    public float SmokeThreshold
    {
        get { return smokeThreshold; }
    }

    public float SkidThreshold
    {
        get { return skidThreshold; }
    }

    public AudioClip TireSkidAudio
    {
        get { return tireSkidAudio; }
    }
}
