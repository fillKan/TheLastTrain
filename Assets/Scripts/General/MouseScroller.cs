using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScroller : MonoBehaviour
{
    [SerializeField] ArrowScroller _Scroller;
    [SerializeField] Transform _TrainTransform;

    private Vector3 mFirstClickPoint;

    private void Update()
    {
        Vector3 MousePoint()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        float Range(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;

            return value;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mFirstClickPoint = MousePoint();
        }
        if (Input.GetMouseButton(0))
        {
            float x = (mFirstClickPoint - MousePoint()).x * 0.02f;

            Vector2 trans = Vector2.left * x + (Vector2)_TrainTransform.position;

            trans.x = Range(trans.x, _Scroller.ScrollRangeMin, _Scroller.ScrollRangeMax);
            trans.y = _TrainTransform.position.y;

            _TrainTransform.position = trans;
        }
    }
}
