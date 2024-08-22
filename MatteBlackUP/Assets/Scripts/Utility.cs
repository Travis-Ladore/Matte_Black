using System;

public static class Utility
{
    private static readonly Random _random = new();

    public static int RandomInt(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }
    
    public static float RandomFloat()
    {
        return  (float)_random.NextDouble();
    }
    public static float RandomFloat(float minValue, float maxValue)
    {
        return (float)_random.NextDouble() * (maxValue - minValue) + minValue;
    }
}