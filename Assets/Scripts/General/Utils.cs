using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    /// <summary>
    /// Convert Discount Target Value To Percent
    /// </summary>
    /// <param name="value">Convert Target Value</param>
    /// <param name="percent">ex: 0.1 = 10%, 0.2 = 20%, 0.3 = 30% ...</param>
    /// <returns></returns>
    public static double GetPercentValue(double value, double percent)
    {
        return value * percent;
    }

    //Convert Percent To Resource Table
    public static short ConvertPercent(uint now, uint max)
    {
        return (short)(((float)now / max) * 100);
    }

    //Convert Percent To Point
    public static float ConvertPercentToPoint(short percentage, int pointUnit)
    {
        return (percentage * Mathf.Pow(0.1f, pointUnit));
    }
}
