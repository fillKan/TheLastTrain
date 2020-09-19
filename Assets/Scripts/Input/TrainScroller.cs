using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.INPUT.Scroll
{
    public class TrainScroller : MonoBehaviour
    {
        public TouchController touchController;
        public Train.Train train;

        private Vector3 startPos;

        [SerializeField] float LimitxMinScroll = 1.0f;
        [SerializeField] float LimitxMaxScroll = 6.0f;
        void OnEnable()
        {
            StartCoroutine(EScroll());
        }

        void OnDisable()
        {
            StopCoroutine(EScroll());
        }

        // 제한 영역 확장
        public float ExpendLimitMinValue(float amount) => (LimitxMinScroll += amount);
        public float ExpendLimitMaxValue(float amount) => (LimitxMaxScroll += amount);

        IEnumerator EScroll()
        {
            while (true)
            {
                switch (touchController.touchState)
                {
                    case TouchState.None:
                        break;
                    case TouchState.Down:
                        PositionInit();
                        break;
                    case TouchState.Drag:
                        Scrolling();
                        break;
                    case TouchState.Up:
                        break;
                    default:
                        break;
                }

                yield return null;
            }
        }
        void PositionInit()
        {
            startPos.x = touchController.InputVector.x - this.transform.localPosition.x;
        }
        void Scrolling()
        {
            if (Time.timeScale <= 0.0f)
                return;


            transform.localPosition = new Vector3(touchController.NextInputVector.x - startPos.x
                , transform.localPosition.y, 0);

            float x = Mathf.Clamp(transform.localPosition.x, LimitxMinScroll, LimitxMaxScroll);
            transform.localPosition = new Vector2(x, transform.localPosition.y);
        }
    }
}