using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IF you use this script, Must Changed Render Mode (Screen Space - Camera)
/// IF you're using 'Camera.main.WorldToScreenPoint', replace it with 'WorldToScreenPointWithCameraSpace' in this script
/// </summary>

[DefaultExecutionOrder(-200)]
public class CameraScreen : Singleton<CameraScreen>
{
    [SerializeField] RectTransform canvasTransfrom = null;

    #region @Variables Field
    Vector2 pos;
    Camera uiCamera;
    Camera worldCamera;
    #endregion

    private void Awake()
    {
        uiCamera = Camera.main;
        worldCamera = Camera.main;
        canvasTransfrom = (canvasTransfrom != null) ? canvasTransfrom : GetComponent<RectTransform>();
    }
    

    public Vector3 WorldToScreenPointWithCameraSpace(Vector3 vector3)
    {
        pos = Vector2.zero;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, vector3);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransfrom, screenPos, uiCamera, out pos);
        return pos;
    }
}
