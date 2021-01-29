using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModule : MonoBehaviour
{
    private IEnumerator _Routine;
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void SmoothPause(float time)
    {
        Time.timeScale = 0;
        if (_Routine != null)
        {
            StopCoroutine(_Routine);
        }
        StartCoroutine(_Routine = SetTimeScaleRoutine(time, 0f));
    }
    public void SmoothResum(float time)
    {
        Time.timeScale = 1;
        if (_Routine != null)
        {
            StopCoroutine(_Routine);
        }
        StartCoroutine(_Routine = SetTimeScaleRoutine(time, 1f));
    }
    private IEnumerator SetTimeScaleRoutine(float time, float targetScale)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, targetScale, Mathf.Min(1f, i / time));
            yield return null;
        }
        Time.timeScale = targetScale;
        _Routine = null;
    }
}
