using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMaster : MonoBehaviour
{
    private Updater[] _Updaters;

    private void Start()
    {
        _Updaters = FindObjectsOfType<Updater>();
    }
    private void Update()
    {
        for (int i = 0; i < _Updaters.Length; ++i)
        {
            _Updaters[i].Update();
        }
    }
}
