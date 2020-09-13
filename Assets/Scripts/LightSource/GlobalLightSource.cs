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
            switch (mRotaryBody.GetTimeOfDay())
            {
                case TimeOfDay.Dawn:
                    mLight2D.color = Color.Lerp(ColorOfNoon, ColorOfDawn, mRotaryBody.Angle / 90f);
                    break;

                case TimeOfDay.Noon:
                    mLight2D.color = Color.Lerp(ColorOfDusk, ColorOfNoon, (mRotaryBody.Angle - 270f) / 90f);
                    break;

                case TimeOfDay.Dusk:
                    mLight2D.color = Color.Lerp(ColorOfMidNight, ColorOfDusk, (mRotaryBody.Angle - 180f) / 90f);
                    break;

                case TimeOfDay.MidNight:
                    mLight2D.color = Color.Lerp(ColorOfDawn, ColorOfMidNight, (mRotaryBody.Angle - 90f) / 90f);
                    break;
            }
            yield return null;
        }
    }
}
