using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightSource : MonoBehaviour
{
    [SerializeField] private Revolution mRevolution;

    [SerializeField] private Light2D mLight2D;

    [SerializeField] private Color ColorOfDawn;
    [SerializeField] private Color ColorOfNoon;
    [SerializeField] private Color ColorOfDuty;
    [SerializeField] private Color ColorOfMidNight;

    private IEnumerator mEUpdate;

    private void Reset()
    {
        ColorOfDawn = Color.white;
        ColorOfNoon = Color.white;
        ColorOfDuty = Color.white;

        ColorOfMidNight = Color.white;

        Debug.Assert(TryGetComponent(out mLight2D));
    }
    private void OnEnable()
    {
        StartCoroutine(mEUpdate = EUpdate());
    }
    private void OnDisable()
    {
        if (mEUpdate != null) {
            StopCoroutine(mEUpdate);
        }
        mEUpdate = null;
    }
    private IEnumerator EUpdate()
    {
        while (gameObject.activeSelf)
        {
            if (mRevolution.Angle <= 90f)
            {
                // Dawn -> Noon
                mLight2D.color = Color.Lerp(ColorOfNoon, ColorOfDawn, mRevolution.Angle / 90f);
            }
            else if (mRevolution.Angle <= 360 && mRevolution.Angle > 270)
            {
                // Noon -> Duty
                mLight2D.color = Color.Lerp(ColorOfDuty, ColorOfNoon, (mRevolution.Angle - 270f) / 90f);
            }
            else if (mRevolution.Angle <= 270 && mRevolution.Angle > 180)
            {
                // Duty -> MidNight
                mLight2D.color = Color.Lerp(ColorOfMidNight, ColorOfDuty, (mRevolution.Angle - 180f) / 90f);
            }
            else if (mRevolution.Angle <= 180 && mRevolution.Angle > 90)
            {
                // MidNight -> Dawn
                mLight2D.color = Color.Lerp(ColorOfDawn, ColorOfMidNight, (mRevolution.Angle - 90f) / 90f);
            }
            yield return null;
        }
    }
}
