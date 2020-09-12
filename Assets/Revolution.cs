using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolution : MonoBehaviour
{
    private float mSpeed
    { get => Time.deltaTime * Time.timeScale * Speed; }

    [SerializeField]
    private float Speed;

    private void OnEnable()
    {
        StartCoroutine(EUpdate());
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
