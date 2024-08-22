using UnityEngine;

[CreateAssetMenu(fileName = "Disc", menuName = "Radio/Disc")]
public class DiscSO : ScriptableObject
{
    [field: SerializeField] public Track[] Tracks { get; private set; } = new Track[]{new Track(false)};
    [field: SerializeField] public Sprite Cover { get; private set; }
}