using UnityEngine;

public class CarAudioController
{
    private readonly CarController _carController;

    private readonly CarControllerSettings _controllerSettings;
    private readonly EngineComponent _engine;
    private readonly GearboxComponent _gearbox;

    private AudioSource _engineAudioSource;
    private readonly AudioSource[] _tireAudioSource = new AudioSource[4];

    public CarAudioController(CarController carController, CarControllerSettings controllerSettings, EngineComponent engine, GearboxComponent gearbox)
    {
        _carController = carController;

        _controllerSettings = controllerSettings;
        _engine = engine;
        _gearbox = gearbox;
        
        SpawnEngineAudio();
        SpawnTireAudio();
    }

    public void Tick()
    {
        PlayEngineSound();
        PlayTireSkidSound();
    }

    private void SpawnEngineAudio()
    {
        _engineAudioSource = _carController.CarTransform.gameObject.AddComponent<AudioSource>();
        _engineAudioSource.clip = _engine.EngineSound;
        _engineAudioSource.loop = true;
        
        _engineAudioSource.Play();
    }

    private void SpawnTireAudio()
    {
        for (int i = 0; i < 4; i++)
        {
            _tireAudioSource[i] = _carController.WheelColliders[i].transform.gameObject.AddComponent<AudioSource>();
            _tireAudioSource[i].clip = _controllerSettings.TireSkidAudio;
            _tireAudioSource[i].loop = true;
            _tireAudioSource[i].volume = 0.2f;
        }
    }

    private void PlayEngineSound()
    {
        _engineAudioSource.pitch = Remap(_carController.Rpm, _gearbox.IdleRpm, _gearbox.UpShiftRpm, _engine.MinEnginePitch, _engine.MaxEnginePitch);
    }

    private void PlayTireSkidSound()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_carController.TireSlip[i] > _controllerSettings.SkidThreshold)
            {
                if(!_tireAudioSource[i].isPlaying) _tireAudioSource[i].Play();
            }
            else
            {
                if(_tireAudioSource[i].isPlaying) _tireAudioSource[i].Stop();
            }
        }
    }
    
    private static float Remap (float value, float inputMin, float inputMax, float outputMin, float outputMax) {
        var remappedValue = (value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
        remappedValue = Mathf.Clamp(remappedValue, outputMin, outputMax);
        return remappedValue;
    }
}
