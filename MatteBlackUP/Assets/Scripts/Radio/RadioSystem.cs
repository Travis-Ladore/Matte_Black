using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.InputSystem;

public class RadioSystem : MonoBehaviour
{
    [SerializeField] private RadioInputReaderSO _inputReader;

    private int _trackIndex;
    private List<Track> _tracks = new();
    
    private int _discIndex;
    private List<DiscSO> _discs = new();

    /*TRACKING*/
    private AudioClip _currentTrack;
    private DiscSO _currentDisc;

    private Coroutine _nextSongCoroutine;
    private const int _songID = 13;

    public static Action<bool, DiscSO> OnDiscChanged;
    public static Action<Track> OnTrackChanged;

    private void OnEnable()
    {
        _inputReader.onTrackChanged += ChangeTrack;
        _inputReader.onDiscChanged += ChangeDisc;
    }
    private void OnDisable()
    {
        _inputReader.onTrackChanged -= ChangeTrack;
        _inputReader.onDiscChanged -= ChangeDisc;
    }

    private void Start()
    {
        Load();
        ChangeDisc(0);
    }

    private void ChangeTrack(InputAction.CallbackContext obj) => ChangeTrack((int)obj.ReadValue<float>());
    private void ChangeDisc(InputAction.CallbackContext obj) => ChangeDisc((int)obj.ReadValue<float>());
    
    private void ChangeDisc(int i)
    {
        if(_discs.Count == 0 || (_discs.Count == 1 && _currentDisc)) return;
        
        _discIndex += i;

        _discIndex = _discIndex >= _discs.Count ? 0 : _discIndex;
        _discIndex = _discIndex < 0 ? _discs.Count - 1 : _discIndex;

        _tracks = new List<Track>(_discs[_discIndex].Tracks);

        _trackIndex = 0;
        _currentTrack = null;
        ChangeTrack(0);

        _currentDisc = _discs[_discIndex];
        
        OnDiscChanged?.Invoke(_discIndex == 0, _currentDisc);
    }
    private void ChangeTrack(int i)
    {
        switch (_tracks.Count)
        {
            case 0:
                MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.FreeTrack, MMSoundManager.MMSoundManagerTracks.Music);
                _currentTrack = null;
                OnTrackChanged?.Invoke(new Track());
                return;
            case 1 when _currentTrack:
                OnTrackChanged?.Invoke(_tracks[_trackIndex]);
                return;
        }

        _trackIndex += i;

        _trackIndex = _trackIndex >= _tracks.Count ? 0 : _trackIndex;
        _trackIndex = _trackIndex < 0 ? _tracks.Count - 1 : _trackIndex;
        
        var current = _tracks[_trackIndex];

        current.Options.ID = _songID;

        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.FreeTrack, MMSoundManager.MMSoundManagerTracks.Music);
        MMSoundManagerSoundPlayEvent.Trigger(current.Clip, current.Options);

        _currentTrack = current.Clip;
        
        if(_nextSongCoroutine != null) StopCoroutine(_nextSongCoroutine);
        _nextSongCoroutine = StartCoroutine(PlayNextTrack(MMSoundManager.Instance.FindByID(_songID)));
        
        OnTrackChanged?.Invoke(_tracks[_trackIndex]);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator PlayNextTrack(AudioSource track)
    {
        yield return new WaitUntil(() => !track.isPlaying);
        
        ChangeTrack(1);
    }
    
    public void CollectDisc(DiscSO disc)
    {
        RadioConfig.Discs.Add(disc);
        _discs = new List<DiscSO>(RadioConfig.Discs);
    }

    public void SwapRadioSignal(DiscSO disc)
    {
        _discs.Insert(0, disc);
        _discs.RemoveAt(1);

        if(_discIndex != 0) return;
        _currentDisc = null;
        _currentTrack = null;
        ChangeDisc(0);
    }
    
    
    public void Save()
    {
        RadioConfig.DiscIndex = _discIndex;
        RadioConfig.TrackIndex = _trackIndex;
    }
    private void Load()
    {
        _discIndex = RadioConfig.DiscIndex ;
        _trackIndex = RadioConfig.TrackIndex;
        
        _discs = new List<DiscSO>(RadioConfig.Discs);
    }
}
