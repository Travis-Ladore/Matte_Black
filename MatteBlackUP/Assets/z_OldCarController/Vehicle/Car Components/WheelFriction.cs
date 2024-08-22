[System.Serializable]
public class TireFrictions
{
    public TireFrictionValues frontWheelFriction;
    public TireFrictionValues rearWheelFriction;
}

[System.Serializable]
public class TireFrictionValues
{
    public WheelFrictionValues forwardFriction;
    public WheelFrictionValues sidewaysFriction;
}

[System.Serializable]
public class WheelFrictionValues
{
    public float extremumSlip;
    public float extremumValue;

    public float asymptoteSlip;
    public float asymptoteValue;

    public float stiffness;
}
