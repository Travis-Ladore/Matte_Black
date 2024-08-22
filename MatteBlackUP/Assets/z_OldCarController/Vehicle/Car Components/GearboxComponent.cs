using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/Gearbox", order = 1)]
public class GearboxComponent : ScriptableObject
{
    [SerializeField] private float[] gearRatios;

    [SerializeField] private int idleRpm;
    [SerializeField] private int redLineRpm;

    [SerializeField] private int upShiftRpm;
    [SerializeField] private int downShiftRpm;

    [SerializeField] private float clutchSpeed;

    [SerializeField] private float gearChangeTime;

    public float[] GearRatios => gearRatios;

    public float IdleRpm => idleRpm;

    public float RedLineRpm => redLineRpm;

    public float UpShiftRpm => upShiftRpm;

    public float DownShiftRpm => downShiftRpm;

    public float ClutchSpeed => clutchSpeed;

    public float GearChangeTime => gearChangeTime;
}
