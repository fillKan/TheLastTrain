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
    [SerializeField] private Color ColorOfDusk;
    [SerializeField] private Color ColorOfMidNight;

    private IEnumerator mEUpdate;

    private void Reset()
    {
        ColorOfDawn = Color.white;
        ColorOfNoon = Color.white;
        ColorOfDusk = Color.white;

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
            float lerpAmount = mRotaryBody.GetPrcentOfNextTime();

            switch (mRotaryBody.GetTimeOfDay())
            {
                case TimeOfDay.Dawn:
                    mLight2D.color = Color.Lerp(ColorOfDawn, ColorOfNoon, lerpAmount);
                    break;

                case TimeOfDay.Noon:
                    mLight2D.color = Color.Lerp(ColorOfNoon, ColorOfDusk, lerpAmount);
                    break;

                case TimeOfDay.Dusk:
                    mLight2D.color = Color.Lerp(ColorOfDusk, ColorOfMidNight, lerpAmount);
                    break;

                case TimeOfDay.MidNight:
                    mLight2D.color = Color.Lerp(ColorOfMidNight, ColorOfDawn, lerpAmount);
                    break;
            }
            yield return null;
        }
    }
}
