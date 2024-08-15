using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/Engine", order = 1)]
public class EngineComponent : ScriptableObject
{
    [SerializeField] private int maxHorsePower;
    [SerializeField] private AnimationCurve powerCurve;

    [SerializeField] private AudioClip engineSound;
    [SerializeField] private float minEnginePitch;
    [SerializeField] private float maxEnginePitch;

    public int MaxHorsePower => maxHorsePower;

    public AnimationCurve PowerCurve => powerCurve;

    public AudioClip EngineSound => engineSound;

    public float MinEnginePitch => minEnginePitch;

    public float MaxEnginePitch => maxEnginePitch;
}
