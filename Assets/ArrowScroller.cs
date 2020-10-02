using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction { Left, Right }

public class ArrowScroller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float Accel;

    [SerializeField] float LimitxMinScroll = 1.0f;
    [SerializeField] float LimitxMaxScroll = 6.0f;

    [SerializeField]
    private Direction ScrollDirection;
    [SerializeField]
    private Transform TrainTransform;

    private bool mIsSelect = false;

    private float DeltaTime
    { get => Time.deltaTime * Time.timeScale * Accel; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            SoundSystem.Instance.PlayOneShot("ArrowTouch");
        }
        mIsSelect = true;
    }
    public void OnPointerUp  (PointerEventData eventData) => mIsSelect = false;

    public float ExpendLimitMinValue(float amount) => LimitxMinScroll += amount;
    public float ExpendLimitMaxValue(float amount) => LimitxMaxScroll += amount;

    private void OnEnable() => StartCoroutine(EUpdate());

    private IEnumerator EUpdate()
    {
        float lerpAmount = 0f;

        while (gameObject.activeSelf)
        { 
            if (mIsSelect)
            {
                lerpAmount = Mathf.Min(1f, lerpAmount + DeltaTime);

                switch (ScrollDirection)
                {
                    case Direction.Left:
                        TrainTransform.localPosition += Vector3.right * Mathf.Lerp(0f, Accel, lerpAmount);

                        TrainTransform.localPosition = 
                            Vector3.Min(TrainTransform.localPosition, new Vector3(LimitxMaxScroll, TrainTransform.localPosition.y, 0));
                        break;

                    case Direction.Right:
                        TrainTransform.localPosition += Vector3.left * Mathf.Lerp(0f, Accel, lerpAmount);

                        TrainTransform.localPosition = 
                            Vector3.Max(TrainTransform.localPosition, new Vector3(LimitxMinScroll, TrainTransform.localPosition.y, 0));
                        break;
                }
            }
            else lerpAmount = 0f;
            
            yield return null;
        }
    }
}
