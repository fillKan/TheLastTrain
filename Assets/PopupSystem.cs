using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSystem : MonoSingleton<PopupSystem>
{
    public Transform parent;

    public GameObject populationPopup;
    public GameObject FoodPopup;
    public GameObject LeadershipPopup;

    public Transform population;
    public Transform food;
    public Transform leadership;


    public GameObject SpawnPopup(string number, ResourceType resourceType)
    {
        GameObject obj = null;
        UnityEngine.UI.Text text;
        switch (resourceType)
        {
            case ResourceType.None:
                break;
            case ResourceType.Population:
                obj = Instantiate(populationPopup, parent);
                text = obj.GetComponentInChildren<UnityEngine.UI.Text>();
                text.text = number;
                return obj;
            case ResourceType.Food:
                obj = Instantiate(FoodPopup, parent);
                text = obj.GetComponentInChildren<UnityEngine.UI.Text>();
                text.text = number;
                return obj;
            case ResourceType.LeaderShip:
                obj = Instantiate(LeadershipPopup, parent);
                text = obj.GetComponentInChildren<UnityEngine.UI.Text>();
                text.text = number;
                return obj;
            default:
                break;
        }
        return null;
    }
}
