using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/CarSpec", order = 1)]
public class CarSpecComponent : ScriptableObject
{
    [SerializeField] private float mass;
    [SerializeField] private Drivetrain driveTrain;

    public float Mass => mass;

    public Drivetrain DriveTrain => driveTrain;
}
