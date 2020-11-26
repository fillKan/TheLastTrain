using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Bubble
{
    public enum SpecialBubbleType
    {
        REBELLION,      //반란
        DEMONSTRATE,        // 시위   
        FALSE_RELIGION      // 사이비
    }
    [System.Serializable]
    public struct RebellionBubble
    {
        public GameObject prefab;
        internal UnityEngine.UI.Button LButton;
        internal UnityEngine.UI.Button RButton;
    }
    [System.Serializable]
    public struct DemonstrateBubble
    {
        public GameObject prefab;
        internal UnityEngine.UI.Button Button;
    }
    [System.Serializable]
    public struct FalseReligionBubble
    {
        public GameObject prefab;
        internal UnityEngine.UI.Button Button;
    }
    public class SpecialBubbleSystem : Singleton<SpecialBubbleSystem>
    {
        public Transform parent;
        public RebellionBubble rebellionBubble;
        public DemonstrateBubble demonstrateBubble;
        public FalseReligionBubble falseReligionBubble;


        [HideInInspector] public ObjectPool m_RebellionPool;
        [HideInInspector] public ObjectPool m_DemonstratePool;
        [HideInInspector] public ObjectPool m_FalseReligionPool;

        public void Start()
        {
            m_RebellionPool = new ObjectPool(rebellionBubble.prefab, 2, parent);
            m_DemonstratePool = new ObjectPool(demonstrateBubble.prefab, 2, parent);
            m_FalseReligionPool = new ObjectPool(falseReligionBubble.prefab, 2, parent);
        }

        public bool SpawnSpecialBubble(SpecialBubbleType type)
        {
            Policy policy = ConvertSpecialBubbleToPolicy(type);
            if (!PolicySystem.Instance.IsExistAccumulatePolicy(policy))
                return false;
            
            GameObject[] bubbleObjects = GameObject.FindGameObjectsWithTag("Bubble") as GameObject[];
            GameObject poolObject = InstantiateSpecialBubble(type);
            poolObject.GetComponent<SpecialBubbleButton>().SpecialBubbleUP();


            PolicySystem.Instance.RemoveAccumulatePolicy(policy);

            Event.SwitchID ConvertedSwitch = ConvertSpecialBubbleToSwitch(type);
            GameEvent.Instance.switchCondition.SwitchOff(ConvertedSwitch);

            StartCoroutine(EUpdate(poolObject, bubbleObjects[Random.Range(0, bubbleObjects.Length)]));
            return true;
        }

        IEnumerator EUpdate(GameObject poolObject, GameObject bubble)
        {
            while (poolObject.activeInHierarchy)
            {
                poolObject.transform.position 
                    = BubbleSystem.ConvertWorldToScreenPoint(bubble.transform.position);
                yield return null;
            }
        }

        [ContextMenu("REBELLION")]
        void SpawnTEST01()
        {
            SpawnSpecialBubble(SpecialBubbleType.REBELLION);
        }

        [ContextMenu("DEMONSTRATE")]
        void SpawnTEST02()
        {
            SpawnSpecialBubble(SpecialBubbleType.DEMONSTRATE);
        }

        [ContextMenu("FALSE_RELIGION")]
        void SpawnTEST03()
        {
            SpawnSpecialBubble(SpecialBubbleType.FALSE_RELIGION);
        }

        public Policy ConvertSpecialBubbleToPolicy(SpecialBubbleType specialBubbleType)
        {
            switch (specialBubbleType)
            {
                case SpecialBubbleType.REBELLION:
                    return Policy.PopulationDownSize;
                case SpecialBubbleType.DEMONSTRATE:
                    return Policy.FoodSaving;
                case SpecialBubbleType.FALSE_RELIGION:
                    return Policy.MissionaryWork;
                default:
                    return Policy.None;
            }
        }

        public Event.SwitchID ConvertSpecialBubbleToSwitch(SpecialBubbleType specialBubbleType)
        {
            switch (specialBubbleType)
            {
                case SpecialBubbleType.REBELLION:
                    return Event.SwitchID.NO7;
                case SpecialBubbleType.DEMONSTRATE:
                    return Event.SwitchID.NO6;
                case SpecialBubbleType.FALSE_RELIGION:
                    return Event.SwitchID.NO5;
                default:
                    return Event.SwitchID.None;
            }
        }


        public GameObject InstantiateSpecialBubble(SpecialBubbleType type)
        {
            GameObject poolObject = null;
            switch (type)
            {
                case SpecialBubbleType.REBELLION:
                    poolObject = m_RebellionPool.pop();
                    break;
                case SpecialBubbleType.DEMONSTRATE:
                    poolObject = m_DemonstratePool.pop();
                    break;
                case SpecialBubbleType.FALSE_RELIGION:
                    poolObject = m_FalseReligionPool.pop();
                    break;
                default:
                    break;
            }

            return poolObject;
        }
    }
}