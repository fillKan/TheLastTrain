using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollElement : MonoBehaviour
{
    public event Action<ScrollElement> InvisibleEvent;

    public Vector2 Head
    {
        get
        {
            Vector2 value = transform.position;
            return  value + Vector2.left * _PivotToHeadLength;
        }
    }
    public Vector2 Tail
    {
        get
        {
            Vector2 value = transform.position;
            return  value + Vector2.right * _PivotToTailLength;
        }
    }

    [SerializeField] private float _PivotToHeadLength;
    [SerializeField] private float _PivotToTailLength;

    private void OnBecameInvisible()
    {
        InvisibleEvent?.Invoke(this);
    }
}
