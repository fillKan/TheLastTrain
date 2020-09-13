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
    [SerializeField] private Color ColorOfDuty;

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
            // Dawn -> Noon
            if (mRotaryBody.Angle <= 90)
            {
                targetColor = ColorOfNoon;
                tracerColor = ColorOfDawn;

                lerpAmount = mRotaryBody.Angle / 90f;                
            }
            // Noon -> Duty
            else if (mRotaryBody.Angle <= 360 && mRotaryBody.Angle > 270)
            {
                targetColor = ColorOfDuty;
                tracerColor = ColorOfNoon;

                lerpAmount = (mRotaryBody.Angle - 270f) / 90f;
            }
            // Duty -> Dawn
            else if (mRotaryBody.Angle <= 270 && mRotaryBody.Angle > 180)
            {
                targetColor = ColorOfDawn;
                tracerColor = ColorOfDuty;

                lerpAmount = (mRotaryBody.Angle - 180f) / 90f;
            }
        }
        else
        {
            // Dawn -> Noon
            if (mRotaryBody.Angle <= 270 && mRotaryBody.Angle > 180)
            {
                targetColor = ColorOfNoon;
                tracerColor = ColorOfDawn;

                lerpAmount = (mRotaryBody.Angle - 180f) / 90f;
            }
            // Noon -> Duty
            else if (mRotaryBody.Angle <= 180 && mRotaryBody.Angle > 90)
            {
                targetColor = ColorOfDuty;
                tracerColor = ColorOfNoon;

                lerpAmount = (mRotaryBody.Angle - 90f) / 90f;
            }
            // Duty -> Dawn
            else if (mRotaryBody.Angle <= 90)
            {
                targetColor = ColorOfDawn;
                tracerColor = ColorOfDuty;

                lerpAmount = mRotaryBody.Angle / 90f;
            }
        }
        if (!targetColor.Equals(Color.clear) && !tracerColor.Equals(Color.clear))
        {
            mLight2D.color = Color.Lerp(targetColor, tracerColor, lerpAmount);
        }       
    }
}
