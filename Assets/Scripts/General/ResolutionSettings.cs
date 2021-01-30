using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettings : MonoBehaviour
{
    private static Camera _camera = null;
    public Camera GetCamera {
        get => (_camera) ? _camera : _camera = Camera.main;
    }
    private readonly Vector2 Resolution = new Vector2(16, 9);
    private void Awake()
    {
        if (Application.isEditor) return;
        Rect rect = GetCamera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)Resolution.x / Resolution.y);
        float scalewidth = 1f / scaleheight;

        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        GetCamera.rect = rect;

        GetCamera.backgroundColor = Color.black;
    }
    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;

    }

}
