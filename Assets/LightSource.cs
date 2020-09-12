using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[System.Serializable]
public struct LightInfo
{
    public Color color;
    public float scale;
}

public class LightSource : MonoBehaviour
{
    [SerializeField] private bool mIsSun;

    [SerializeField] private Revolution mRevolution;

    [SerializeField] private Light2D mLight2D;

    [SerializeField] private Color ColorOfDawn;
    [SerializeField] private Color ColorOfNoon;
    [SerializeField] private Color ColorOfDuty;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out mLight2D));
    }
    private void Update()
    {
        if (mIsSun)
        {
            if (mRevolution.Angle <= 90)
            {
                // Dawn -> Noon
            }
            else if (mRevolution.Angle <= 360 && mRevolution.Angle > 270)
            {
                // Noon -> Duty
            }
            else if (mRevolution.Angle <= 270 && mRevolution.Angle > 180)
            {
                // Duty -> Dawn
            }
        }
        else
        {  
            if (mRevolution.Angle <= 270 && mRevolution.Angle > 180)
            {
                // Dawn -> Noon
            }
            else if (mRevolution.Angle <= 180 && mRevolution.Angle > 90)
            {
                // Noon -> Duty
            }
            else if (mRevolution.Angle <= 90)
            {
                // Duty -> Dawn
            }
        }
    }
}
