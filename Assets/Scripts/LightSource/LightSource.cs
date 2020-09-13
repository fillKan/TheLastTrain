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
        float lerpAmount = 0f;

        Color targetColor = Color.clear;
        Color tracerColor = Color.clear;

        if (mIsSun)
        {
            switch (mRotaryBody.GetTimeOfDay())
            {
                case TimeOfDay.Dawn:
                    targetColor = ColorOfNoon;
                    tracerColor = ColorOfDawn;

                    lerpAmount = mRotaryBody.Angle / 90f;
                    break;

                case TimeOfDay.Noon:
                    targetColor = ColorOfDusk;
                    tracerColor = ColorOfNoon;

                    lerpAmount = (mRotaryBody.Angle - 270f) / 90f;
                    break;

                case TimeOfDay.Dusk:
                    targetColor = ColorOfDawn;
                    tracerColor = ColorOfDusk;

                    lerpAmount = (mRotaryBody.Angle - 180f) / 90f;
                    break;
            }
        }
        else
        {
            switch (mRotaryBody.GetTimeOfDay())
            {
                case TimeOfDay.Dawn:
                    targetColor = ColorOfDawn;
                    tracerColor = ColorOfDusk;

                    lerpAmount = mRotaryBody.Angle / 90f;
                    break;

                case TimeOfDay.Dusk:
                    targetColor = ColorOfNoon;
                    tracerColor = ColorOfDawn;

                    lerpAmount = (mRotaryBody.Angle - 180f) / 90f;
                    break;

                case TimeOfDay.MidNight:
                    targetColor = ColorOfDusk;
                    tracerColor = ColorOfNoon;

                    lerpAmount = (mRotaryBody.Angle - 90f) / 90f;
                    break;
            }
        }
        if (!targetColor.Equals(Color.clear) && !tracerColor.Equals(Color.clear))
        {
            mLight2D.color = Color.Lerp(targetColor, tracerColor, lerpAmount);
        }       
    }
}
