using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace InGame.Touch
{
    public enum TouchState
    {
        None,
        Down,
        Drag,
        Up
    }
    public class TouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public TouchState touchState = TouchState.None;

        private Vector3 _inputVector;
        public Vector3 InputVector
        {
            get
            {
                return _inputVector;
            }
        }

        private Vector3 _nextInputVector;
        public Vector3 NextInputVector
        {
            get
            {
                return _nextInputVector;
            }
        }

        #region InputFunctions class : TouchController
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            _inputVector = Input.mousePosition;
            _inputVector = Camera.main.ScreenToWorldPoint(_inputVector);

            touchState = TouchState.Down;
        }
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            _nextInputVector = Input.mousePosition;
            _nextInputVector = Camera.main.ScreenToWorldPoint(_nextInputVector);

            touchState = TouchState.Drag;
        }
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
            touchState = TouchState.Up;
        }
        #endregion
    }
}