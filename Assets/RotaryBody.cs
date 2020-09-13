using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryBody : MonoBehaviour
{
    public float Angle
    { get => transform.rotation.eulerAngles.z; }

    private float mSpeed
    { get => Time.deltaTime * Time.timeScale * Speed; }

    [SerializeField]
    private float Speed;

    private IEnumerator mEUpdate;

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
