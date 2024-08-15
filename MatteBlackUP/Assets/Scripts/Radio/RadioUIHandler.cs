using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadioUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _songName;
    [SerializeField] private Image _cdCover;
    [SerializeField] private Image _indicator;

    private void OnEnable()
    {
        RadioSystem.OnDiscChanged += OnDiscChanged;
        RadioSystem.OnTrackChanged += OnTrackChanged;
    }
    private void OnDisable()
    {
        RadioSystem.OnDiscChanged -= OnDiscChanged;
        RadioSystem.OnTrackChanged -= OnTrackChanged;
    }

    
    private void OnDiscChanged(bool radio, DiscSO disc)
    {
        _cdCover.sprite = disc.Cover;
        _indicator.enabled = !radio;
    }
    
    private void OnTrackChanged(Track track)
    {
        _songName.text = track.DisplayedName;
    }
}
