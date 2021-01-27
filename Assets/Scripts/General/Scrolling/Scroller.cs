using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private Vector2 mDirection = Vector2.right;
    [SerializeField] private float mSpeed;

    private void Update()
    {
        float deltaTime = Time.deltaTime * Time.timeScale;

        transform.position -= (Vector3)mDirection * (mSpeed * deltaTime) * 20f;
    }
}
