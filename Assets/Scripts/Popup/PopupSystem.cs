using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupPointBase<T>
{
    public T populationPopup;
    public T FoodPopup;
    public T LeadershipPopup;
}

[System.Serializable]
public class PopupObjectPoint : PopupPointBase<GameObject> { }

[System.Serializable]
public class PopupTransformPoint : PopupPointBase<Transform> { }

[System.Serializable]
public class PopupPositionPoint : PopupPointBase<Vector2> { }



public class PopupSystem : MonoBehaviour
{
    [UnityEngine.Serialization.FormerlySerializedAs("parent")]
    [SerializeField] Transform mParent;

    [SerializeField] PopupObjectPoint    mPopupPrefabs;
    [SerializeField] PopupTransformPoint mPopupTransform;
    public PopupPositionPoint mPopupPoint;

    private bool PositionCaching()
    {
        if (mPopupTransform.populationPopup == null
            && mPopupTransform.FoodPopup == null
            && mPopupTransform.LeadershipPopup == null)
            return false;

        mPopupPoint.populationPopup = CameraScreen.Instance.WorldToScreenPointWithCameraSpace(mPopupTransform.populationPopup.position);
        mPopupPoint.FoodPopup       = CameraScreen.Instance.WorldToScreenPointWithCameraSpace(mPopupTransform.FoodPopup.position);
        mPopupPoint.LeadershipPopup = CameraScreen.Instance.WorldToScreenPointWithCameraSpace(mPopupTransform.LeadershipPopup.position);
        return true;
    }

    public GameObject SpawnPopup(string number, ResourceType resourceType)
    {
        PositionCaching();


        GameObject obj = null;
        UnityEngine.UI.Text text;
        switch (resourceType)
        {
            case ResourceType.None:
                break;
            case ResourceType.Population:
                obj = Instantiate(mPopupPrefabs.populationPopup, mParent);
                obj.transform.localPosition = mPopupPoint.populationPopup;

                text = obj.GetComponentInChildren<UnityEngine.UI.Text>();
                text.text = number;
                return obj;
            case ResourceType.Food:
                obj = Instantiate(mPopupPrefabs.FoodPopup, mParent);
                obj.transform.localPosition = mPopupPoint.FoodPopup;

                text = obj.GetComponentInChildren<UnityEngine.UI.Text>();
                text.text = number;
                return obj;
            case ResourceType.LeaderShip:
                obj = Instantiate(mPopupPrefabs.LeadershipPopup, mParent);
                obj.transform.localPosition = mPopupPoint.LeadershipPopup;

                text = obj.GetComponentInChildren<UnityEngine.UI.Text>();
                text.text = number;
                return obj;
            default:
                break;
        }
        return null;
    }
}
