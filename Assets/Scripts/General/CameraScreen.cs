using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    public Vector3 ConvertWorldToScreenPoint(Vector3 vector3)
    {
        pos = Vector2.zero;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, vector3);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransfrom, screenPos, uiCamera, out pos);
        return pos;
    }
}
