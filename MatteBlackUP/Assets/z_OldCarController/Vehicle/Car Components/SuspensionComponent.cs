using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/Suspension", order = 1)]
public class SuspensionComponent : ScriptableObject
{
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private float wheelOffset;

    public float MaxSteeringAngle => maxSteeringAngle;

    public float WheelOffset => wheelOffset;
}
