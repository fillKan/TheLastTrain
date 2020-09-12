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

    [SerializeField] private LightInfo LightOfMidDay;
    [SerializeField] private LightInfo LightOfMidNight;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out mLight2D));
    }
    private void Update()
    {
        if (mIsSun)
        {
            if (mRevolution.Angle <= 180)
            {
                mLight2D.pointLightOuterRadius = Mathf.Lerp(LightOfMidDay.scale, LightOfMidNight.scale, mRevolution.Angle / 180);

                mLight2D.color = Color.Lerp(LightOfMidDay.color, LightOfMidNight.color, mRevolution.Angle / 180);
            }
            else if (mRevolution.Angle <= 360)
            {
                mLight2D.pointLightOuterRadius = Mathf.Lerp(LightOfMidNight.scale, LightOfMidDay.scale, (mRevolution.Angle - 180f) / 180);

                mLight2D.color = Color.Lerp(LightOfMidNight.color, LightOfMidDay.color, (mRevolution.Angle - 180f) / 180);
            }
        }
        else
        {
            if (mRevolution.Angle <= 180)
            {
                mLight2D.pointLightOuterRadius = Mathf.Lerp(LightOfMidNight.scale, LightOfMidDay.scale, mRevolution.Angle / 180);

                mLight2D.color = Color.Lerp(LightOfMidNight.color, LightOfMidDay.color, mRevolution.Angle / 180);
            }
            else if (mRevolution.Angle <= 360)
            {
                mLight2D.pointLightOuterRadius = Mathf.Lerp(LightOfMidDay.scale, LightOfMidNight.scale, (mRevolution.Angle - 180f) / 180);

                mLight2D.color = Color.Lerp(LightOfMidDay.color, LightOfMidNight.color, (mRevolution.Angle - 180f) / 180);
            }
        }
    }
}
