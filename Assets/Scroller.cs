using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float DeltaTime
    { get => Time.deltaTime * Time.timeScale; }

    private Renderer mRenderer;

    [SerializeField]
    private Vector2 mDirection = Vector2.right;

    [SerializeField] 
    private float mSpeed;
    private float mOffset;

    private void OnEnable()
    {
        mOffset = 0f;

        Debug.Assert(TryGetComponent(out mRenderer));
    }
    private void Update()
    {
        mRenderer.material.mainTextureOffset += mDirection * (mSpeed * DeltaTime) * 0.12f;
    }
}
