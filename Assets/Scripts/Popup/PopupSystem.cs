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

[System.Serializable]
public class PopupObjectPool : PopupPointBase<ObjectPool> { }

public class PopupSystem : MonoBehaviour
{
    [UnityEngine.Serialization.FormerlySerializedAs("parent")]
    [SerializeField] Transform mParent;

    [SerializeField] PopupObjectPoint    mPopupPrefabs;
    [SerializeField] PopupTransformPoint mPopupTransform;
    public PopupPositionPoint mPopupPoint { get; private set; }
    public PopupObjectPool mPopupObjectPool { get; private set; }

    private void Awake()
    {
        mPopupPoint = new PopupPositionPoint();
        mPopupObjectPool = new PopupObjectPool();
        mPopupObjectPool.populationPopup = new ObjectPool(mPopupPrefabs.populationPopup, 2, mParent);
        mPopupObjectPool.FoodPopup       = new ObjectPool(mPopupPrefabs.FoodPopup, 2, mParent);
        mPopupObjectPool.LeadershipPopup = new ObjectPool(mPopupPrefabs.LeadershipPopup, 2, mParent);
    }

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
        InGame.Bubble.BubbleSystem.Instance.OnClickBubbleAny?.Invoke();

        PositionCaching();

        ObjectPool objectPool = null;
        GameObject obj = null;
        Popup popup;
        switch (resourceType)
        {
            case ResourceType.None:
                break;
            case ResourceType.Population:
                objectPool = mPopupObjectPool.populationPopup;

                obj = objectPool.pop();
                obj.transform.localPosition = mPopupPoint.populationPopup;

                popup = obj.GetComponentInChildren<Popup>();
                popup.popupText.text = number;
                popup.SetPool(objectPool);
                return obj;
            case ResourceType.Food:
                objectPool = mPopupObjectPool.FoodPopup;

                obj = objectPool.pop();
                obj.transform.localPosition = mPopupPoint.FoodPopup;

                popup = obj.GetComponentInChildren<Popup>();
                popup.popupText.text = number;
                popup.SetPool(objectPool);
                return obj;
            case ResourceType.LeaderShip:
                objectPool = mPopupObjectPool.LeadershipPopup;

                obj = objectPool.pop();
                obj.transform.localPosition = mPopupPoint.LeadershipPopup;

                popup = obj.GetComponentInChildren<Popup>();
                popup.popupText.text = number;
                popup.SetPool(objectPool);
                return obj;
            default:
                break;
        }
        return null;
    }
}
