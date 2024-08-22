using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/Tire", order = 1)]
public class TireComponent : ScriptableObject
{
    [SerializeField] private TireFrictions racingWheelFriction;
    [SerializeField] private TireFrictions driftWheelFriction;

    public TireFrictions RacingWheelFriction => racingWheelFriction;

    public TireFrictions DriftWheelFriction => driftWheelFriction;
}
