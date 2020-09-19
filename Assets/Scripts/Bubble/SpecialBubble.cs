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
    public class SpecialBubble : Singleton<SpecialBubble>
    {
        public Transform parent;
        public RebellionBubble rebellionBubble;
        public DemonstrateBubble demonstrateBubble;
        public FalseReligionBubble falseReligionBubble;


        [HideInInspector] public objectPool m_RebellionPool;
        [HideInInspector] public objectPool m_DemonstratePool;
        [HideInInspector] public objectPool m_FalseReligionPool;

        public void Start()
        {
            m_RebellionPool = new objectPool(rebellionBubble.prefab, 2, parent);
            m_DemonstratePool = new objectPool(demonstrateBubble.prefab, 2, parent);
            m_FalseReligionPool = new objectPool(falseReligionBubble.prefab, 2, parent);
        }

        public void SpawnSpecialBubble(SpecialBubbleType type)
        {
            GameObject[] bubbleObjects = GameObject.FindGameObjectsWithTag("Bubble") as GameObject[];
            GameObject poolObject = InstantiateSpecialBubble(type);
            poolObject.GetComponent<SpecialBubbleButton>().SpecialBubbleUP();
            StartCoroutine(EUpdate(poolObject, bubbleObjects[Random.Range(0, bubbleObjects.Length)]));
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