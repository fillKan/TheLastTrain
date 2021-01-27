using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Scroller : MonoBehaviour
{
    [SerializeField] private float _DirectionX;
    [FormerlySerializedAs("mSpeed")]
    [SerializeField] private float _Speed;

    [SerializeField] private ScrollElement[] _ScrollElement;

    private void Awake()
    {
        for (int i = 0; i < _ScrollElement.Length; ++i)
        {
            _ScrollElement[i].InvisibleEvent += o =>
            {
                for (int j = 0; j < _ScrollElement.Length; ++j)
                {
                    if (_ScrollElement[j].Equals(o))
                    {
                        o.SetHeadToTail(_ScrollElement[j - 1 < 0 ? _ScrollElement.Length - 1 : j - 1]);
                        return;
                    }
                }
            };
        }
    }
    private void Scrolling(int index, ScrollElement element)
    {
        element.SetHeadToTail(_ScrollElement[index]);
    }
    private void Update()
    {
        float deltaTime = Time.deltaTime * Time.timeScale;

        for (int i = 0; i < _ScrollElement.Length; ++i)
        {
            _ScrollElement[i].Scrolling(_DirectionX * _Speed * deltaTime * 20f);
        }
    }
}
