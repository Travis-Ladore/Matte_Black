using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private readonly Dictionary<SFXDataSO, int> Indexes = new();

    public void PlaySound(SFXDataSO data)
    {
        Indexes.TryAdd(data, 0);

        var currentSFX = data.PlayRandomly
            ? data.SoundEffects[Utility.RandomInt(0, data.SoundEffects.Length)]
            : data.SoundEffects[Indexes[data]];

        Indexes[data] = Indexes[data] >= data.SoundEffects.Length - 1 ? 0 : Indexes[data] + 1;  

        MMSoundManagerSoundPlayEvent.Trigger(currentSFX.Clip, currentSFX.Options);
    }
}