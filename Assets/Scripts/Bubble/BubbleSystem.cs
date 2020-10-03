using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace InGame
{
    public enum Vehicles
    {
        GUESTROOM,         // 객실 (Population)
        CULTIVATION,       // 재배실(FOOD)
        EDUCATION,        // 교육실 (LeaderShip)
        PRESERVE,
        STORAGE
    }
}

namespace InGame.Bubble
{
    [System.Serializable]
    public class BubbleTable
    {
        public GameObject gameobject;
        public float bubbleUpTime;
        public bool isTimeup = true;
        internal Button button;
        internal Bubble bubble;
        internal IBubble ibubble;
    }


    internal class BubbleSystem : Singleton<BubbleSystem>
    {
        
        [SerializeField] Transform objectPoolParent;
        public BubbleTable[] BubbleTables;

        public float BubbleEndTime = 7.0f;
        public bool IsAbleProduce = true;


        [HideInInspector] public Bubble[] bubbles;
        [HideInInspector] public ObjectPool m_PopulationPool;
        [HideInInspector] public ObjectPool m_FoodPool;
        [HideInInspector] public ObjectPool m_LeaderShipPool;

        void Awake()
        {
            bubbles = FindObjectsOfType<Bubble>();
            for (int i = 0; i < BubbleTables.Length; i++)
            {
                Debug.Assert(BubbleTables[i].gameobject.TryGetComponent(out BubbleTables[i].button)
                , $"<color=red>GameObject '{BubbleTables[i].gameobject.name}' is Wrong</color>");
            }

            m_PopulationPool = new ObjectPool(BubbleTables[(int)Vehicles.GUESTROOM].gameobject, 2, objectPoolParent);
            m_FoodPool = new ObjectPool(BubbleTables[(int)Vehicles.CULTIVATION].gameobject, 5, objectPoolParent);
            m_LeaderShipPool = new ObjectPool(BubbleTables[(int)Vehicles.EDUCATION].gameobject, 2, objectPoolParent);
        }
        public static bool SetActive(GameObject @object, bool active)
        {
            @object.SetActive(active);
            if (@object.activeSelf == active)
                return true;
            return false;
        }
        public static Vector3 ConvertWorldToScreenPoint(Vector3 vector3)
        {
            return Camera.main.WorldToScreenPoint(vector3);
        }
    }

}