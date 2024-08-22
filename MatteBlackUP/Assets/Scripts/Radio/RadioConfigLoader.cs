using System.Collections.Generic;
using UnityEngine;

public class RadioConfigLoader : MonoBehaviour
{
    [SerializeField] private List<DiscSO> _initialDiscs;
    [SerializeField] private int _trackIndex;
    [SerializeField] private int _discIndex;

    private void Awake()
    {
        RadioConfig.Discs = _initialDiscs;
        RadioConfig.TrackIndex = _trackIndex;
        RadioConfig.DiscIndex = _discIndex;
    }
}
