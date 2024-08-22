using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSpeedMeter : MonoBehaviour
{
    [SerializeField] private Slider rpmGreenZone;
    [SerializeField] private Slider rpmRedZone;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI gearText;

    public void SetUpRevMeter(float upShiftRpm, float redLineRpm)
    {
        rpmGreenZone.maxValue = rpmRedZone.minValue = upShiftRpm;
        rpmRedZone.maxValue = redLineRpm;
    }

    public void UpdateRevMeter(float rpm)
    {
        rpmGreenZone.value = rpmRedZone.value = rpm;
    }

    public void UpdateSpeedText(float speed)
    {
        speedText.text = speed.ToString("000");
    }

    public void UpdateGearText(string currentGear)
    {
        gearText.text = currentGear;
    }
}
