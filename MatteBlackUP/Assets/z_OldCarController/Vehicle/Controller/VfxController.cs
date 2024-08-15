using UnityEngine;

public class VfxController
{
    private readonly CarController _carController;
    private readonly CarControllerSettings _controllerSettings;

    private readonly ParticleSystem[] _tireSmoke = new ParticleSystem[4];
    private readonly TrailRenderer[] _tireSkids = new TrailRenderer[4];

    public VfxController(CarController carController, CarControllerSettings controllerSettings, GameObject tireSmokeParticle, GameObject tireSkidMarks)
    {
        _carController = carController;
        _controllerSettings = controllerSettings;

        for (var i = 0; i < 4; i++)
        {
            var wheelPosition = _carController.WheelColliders[i].transform.position;

            var spawnPosition = new Vector3(wheelPosition.x, wheelPosition.y - _carController.WheelColliders[i].radius + 0.1f, wheelPosition.z);

            _tireSmoke[i] =
                Object.Instantiate(tireSmokeParticle, spawnPosition, _carController.CarTransform.rotation).GetComponent<ParticleSystem>();
            _tireSmoke[i].transform.parent = _carController.WheelColliders[i].transform;

            _tireSkids[i] = Object.Instantiate(tireSkidMarks, spawnPosition, Quaternion.Euler(90f, 0f, 0f))
                .GetComponent<TrailRenderer>();
            _tireSkids[i].transform.parent = _carController.WheelColliders[i].transform;
        }
    }

    public void Tick()
    {
        for (var i = 0; i < 4; i++)
        {
            if (_carController.TireSlip[i] > _controllerSettings.SmokeThreshold)
            {
                _tireSmoke[i].Play();
            }
            else
            {
                _tireSmoke[i].Stop();
            }

            _tireSkids[i].emitting = _carController.TireSlip[i] > _controllerSettings.SkidThreshold;
        }
    }
}
