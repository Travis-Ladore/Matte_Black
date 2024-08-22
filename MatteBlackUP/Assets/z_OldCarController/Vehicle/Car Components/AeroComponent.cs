using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CarComponents/Aero", order = 1)]
public class AeroComponent : ScriptableObject
{
    [SerializeField] private float maxDownforce;

    public float MaxDownforce => maxDownforce;
}
