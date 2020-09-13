using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightSource : MonoBehaviour
{
    [SerializeField] private RotaryBody mRotaryBody;

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
            // Dawn -> Noon
            if (mRotaryBody.Angle <= 90f)
            {
                mLight2D.color = Color.Lerp(ColorOfNoon, ColorOfDawn, mRotaryBody.Angle / 90f);
            }
            // Noon -> Duty
            else if (mRotaryBody.Angle <= 360 && mRotaryBody.Angle > 270)
            {
                mLight2D.color = Color.Lerp(ColorOfDuty, ColorOfNoon, (mRotaryBody.Angle - 270f) / 90f);
            }
            // Duty -> MidNight
            else if (mRotaryBody.Angle <= 270 && mRotaryBody.Angle > 180)
            {
                mLight2D.color = Color.Lerp(ColorOfMidNight, ColorOfDuty, (mRotaryBody.Angle - 180f) / 90f);
            }
            // MidNight -> Dawn
            else if (mRotaryBody.Angle <= 180 && mRotaryBody.Angle > 90)
            {
                mLight2D.color = Color.Lerp(ColorOfDawn, ColorOfMidNight, (mRotaryBody.Angle - 90f) / 90f);
            }
            yield return null;
        }
    }
}
