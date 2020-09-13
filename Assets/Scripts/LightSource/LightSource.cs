using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSource : MonoBehaviour
{
    [SerializeField] private bool mIsSun;

    [SerializeField] private RotaryBody mRotaryBody;

    [SerializeField] private Light2D mLight2D;

    [SerializeField] private Color ColorOfDawn;
    [SerializeField] private Color ColorOfNoon;
    [SerializeField] private Color ColorOfDusk;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out mLight2D));
    }
    private void Update()
    {
        Color targetColor = Color.clear;
        Color tracerColor = Color.clear;

        if (mIsSun)
        {
            switch (mRotaryBody.GetTimeOfDay())
            {
                case TimeOfDay.Dawn:
                    targetColor = ColorOfDawn;
                    tracerColor = ColorOfNoon;
                    break;

                case TimeOfDay.Noon:
                    targetColor = ColorOfNoon;
                    tracerColor = ColorOfDusk;
                    break;

                case TimeOfDay.Dusk:
                    targetColor = ColorOfDusk;
                    tracerColor = ColorOfDawn;
                    break;
            }
        }
        else
        {
            switch (mRotaryBody.GetTimeOfDay())
            {
                case TimeOfDay.Dawn:
                    targetColor = ColorOfDusk;
                    tracerColor = ColorOfDawn;
                    break;

                case TimeOfDay.Dusk:
                    targetColor = ColorOfDawn;
                    tracerColor = ColorOfNoon;
                    break;

                case TimeOfDay.MidNight:
                    targetColor = ColorOfNoon;
                    tracerColor = ColorOfDusk;
                    break;
            }
        }
        if (!targetColor.Equals(Color.clear) && !tracerColor.Equals(Color.clear))
        {
            mLight2D.color = Color.Lerp(targetColor, tracerColor, mRotaryBody.GetPrcentOfNextTime());
        }       
    }
}
