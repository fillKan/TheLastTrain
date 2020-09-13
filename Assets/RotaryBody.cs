using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeOfDay
{
    Dawn, Noon, Dusk, MidNight
}

public class RotaryBody : MonoBehaviour
{
    public float Angle
    { get => transform.rotation.eulerAngles.z; }

    private float mSpeed
    { get => Time.deltaTime * Time.timeScale * Speed; }

    [SerializeField]
    private float Speed;

    private IEnumerator mEUpdate;

    public TimeOfDay GetTimeOfDay()
    {
        if (Angle <= 90) {
            return TimeOfDay.Dawn;
        }
        else if (Angle <= 360 && Angle >= 270) {
            return TimeOfDay.Noon;
        }
        else if (Angle <= 270 && Angle >= 180) {
            return TimeOfDay.Dusk;
        }
        else {
            return TimeOfDay.MidNight;
        }        
    }
    public float GetPrcentOfNextTime()
    {
        switch (GetTimeOfDay())
        {
            case TimeOfDay.Dawn:
                return ( 90f - Angle) /  90f;
            case TimeOfDay.Noon:
                return (180f - (Angle - 180f)) / 90f;
            case TimeOfDay.Dusk:
                return (180f - (Angle -  90f)) / 90f;
            case TimeOfDay.MidNight:
                return ( 90f - (Angle -  90f)) / 90f;
        }
        return 0f;
    }

    private void OnEnable()
    {
        StartCoroutine(mEUpdate = EUpdate());
    }
    private void OnDisable()
    {
        if (mEUpdate != null)
        {
            StopCoroutine(mEUpdate);

            mEUpdate = null;
        }
    }
    private IEnumerator EUpdate()
    {
        while (gameObject.activeSelf)
        {
            transform.rotation = Quaternion.Euler(Vector3.back * mSpeed + transform.rotation.eulerAngles);

            yield return null;
        }
    }
}
