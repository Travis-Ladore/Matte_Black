using System.Threading.Tasks;
using UnityEngine;

public class GearboxController
{
    private readonly CarController _carController;
    
    private readonly GearboxComponent _gearbox;

    public GearboxController(CarController carController, GearboxComponent gearbox)
    {
        _carController = carController;
        
        _gearbox = gearbox;
    }

    public void Tick()
    {
        AutoClutch();
        CheckGearChange();
    }
    
    private void AutoClutch()
    {
        if (_carController.CurrentGearState != GearState.ChangingGear)
        {
            var clutchDelta = Mathf.Clamp01(_carController.Clutch + (Time.deltaTime * _gearbox.ClutchSpeed));
            
            _carController.Clutch = clutchDelta;
        }
        else if (_carController.CurrentGearState == GearState.ChangingGear)
        {
            _carController.Clutch = 0f;
        }
    }
    
    private void CheckGearChange()
    {
        if (_carController.CurrentGearState != GearState.Running || !(_carController.Clutch > 0)) return;
        
        if (_carController.Rpm > _gearbox.UpShiftRpm)
        {
            ChangeGear(1);
        }
        else if (_carController.Rpm < _gearbox.DownShiftRpm)
        {
            ChangeGear(-1);
        }
    }
    
    private async void ChangeGear(int gearChange)
    {
        UpdateGearState(GearState.CheckingChange);
        if (_carController.CurrentGear + gearChange >= 0)
        {
            await Task.Delay(500);
            
            switch (gearChange)
            {
                case > 0 when _carController.Rpm < _gearbox.UpShiftRpm || _carController.CurrentGear >= _gearbox.GearRatios.Length - 1:
                    UpdateGearState(GearState.Running);
                    await Task.Yield();
                    return;
                case < 0 when _carController.Rpm > _gearbox.DownShiftRpm || _carController.CurrentGear <= 0:
                    UpdateGearState(GearState.Running);
                    await Task.Yield();
                    return;
            }
            
            UpdateGearState(GearState.ChangingGear);
            
            var gearChangeDelay = (int) _gearbox.GearChangeTime * 1000;
            await Task.Delay(gearChangeDelay);
            
            _carController.CurrentGear += gearChange;
        }
        
        UpdateGearState(GearState.Running);
    }

    private void UpdateGearState(GearState newGearState)
    {
        _carController.CurrentGearState = newGearState;
    }
}
