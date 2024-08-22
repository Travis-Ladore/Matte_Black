using MoreMountains.Tools;
using UnityEngine;

[System.Serializable]
public struct Track
{
    public bool IsSFX;
    public string DisplayedName;
    public AudioClip Clip;
    public MMSoundManagerPlayOptions Options;

    public Track(bool isSFX)
    {
        Options = MMSoundManagerPlayOptions.Default;
        IsSFX = isSFX;
        DisplayedName = null;
        Clip = null;
        
        
        if (!isSFX) Options.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Music;
    }
}