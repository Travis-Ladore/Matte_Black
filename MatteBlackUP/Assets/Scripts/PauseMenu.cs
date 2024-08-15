using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private UIInputReaderSO _inputReader;
    
    private bool _paused;


    private void OnEnable()
    {
        _inputReader.onGamePaused += Pause;
    }

    public void Pause()
    {
        _paused = !_paused;
        
        _pauseMenu.SetActive(_paused);

        Time.timeScale = _paused ? 0 : 1;
    }
}
