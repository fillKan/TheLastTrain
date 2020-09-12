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
    [SerializeField] private Light2D mLight2D;

    [SerializeField] private LightInfo LightOfMidDay;
    [SerializeField] private LightInfo LightOfMidNight;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out mLight2D));
    }
}
