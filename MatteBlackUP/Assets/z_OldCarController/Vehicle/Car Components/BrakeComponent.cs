using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/Brakes", order = 1)]
public class BrakeComponent : ScriptableObject
{
    [SerializeField] private int brakePower;
    [Range(0f, 1f)] public float brakeBias;
    
    public int BrakePower => brakePower;

    public float BrakeBias => brakeBias;
}
