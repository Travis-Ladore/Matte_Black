using UnityEngine;

[CreateAssetMenu(menuName = "Radio/SFX")]
public class SFXDataSO : ScriptableObject
{
    public bool PlayRandomly;
    public Track[] SoundEffects ={new(true)};
}