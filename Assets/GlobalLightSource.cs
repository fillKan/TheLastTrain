using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightSource : MonoBehaviour
{
    [SerializeField] private Light2D mLight2D;

    [SerializeField] private Color ColorOfDawn;
    [SerializeField] private Color ColorOfNoon;
    [SerializeField] private Color ColorOfDuty;
    [SerializeField] private Color ColorOfMidNight;
}
