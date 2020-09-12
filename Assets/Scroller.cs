using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float DeltaTime
    { get => Time.deltaTime * Time.timeScale; }

    private Renderer mRenderer;

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
        mOffset += mSpeed * DeltaTime;

        mRenderer.material.SetTextureOffset("_MainTex", Vector2.right * mOffset);
    }
}
