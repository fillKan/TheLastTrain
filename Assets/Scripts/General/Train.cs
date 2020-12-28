using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameBubble = InGame.Bubble;

namespace InGame.Train
{
    public struct TrainAmount
    {
        public uint GuestRoom;
        public uint Cultivation;
        public uint Education;
        public uint Preserve;
        public uint Storage;
    }

    public class Train : MonoBehaviour
    {
        [SerializeField] private ArrowScroller[] arrowScrollers = new ArrowScroller[2];
        [Space]

        [SerializeField] private Transform InitPos;
        [SerializeField] private GameObject GuestRoomPrefabs;
        [SerializeField] private GameObject CultivationPrefabs;
        [SerializeField] private GameObject EducationPrefabs;
        [SerializeField] private GameObject PreservationPrefabs;
        [SerializeField] private GameObject StoragePrefabs;

        private ObjectPool m_GuestRoomPool;
        private ObjectPool m_CultivationPool;
        private ObjectPool m_EducationPool;
        private ObjectPool m_PreservationPool;
        private ObjectPool m_StoragePool;

        [SerializeField] float SpawnSpacing = -4;
        [SerializeField] float ExpendMinAmount = -2.0f;
        [SerializeField] float ExpendMaxAmount = 2.0f;

        InGameBubble.Bubble[] bubbles;

        private TrainAmount trainAmount;
        public TrainAmount GetTrainAmount() => trainAmount;
        public void ApplyGuestRoomAmount(int Amount) => trainAmount.GuestRoom = (uint)Mathf.Max(0, trainAmount.GuestRoom + Amount);
        public void ApplyCultivationAmount(int Amount) => trainAmount.Cultivation = (uint)Mathf.Max(0, trainAmount.Cultivation + Amount);
        public void ApplyEducationAmount(int Amount) => trainAmount.Education = (uint)Mathf.Max(0, trainAmount.Education + Amount);
        public void ApplyPreserveAmount(int Amount) => trainAmount.Preserve = (uint)Mathf.Max(0, trainAmount.Preserve + Amount);
        public void ApplyStorageAmount(int Amount) => trainAmount.Storage = (uint)Mathf.Max(0, trainAmount.Storage + Amount);

        public void DisableAnimation()
        {
            if (gameObject.TryGetComponent(out Animator animator)) {
                animator.enabled = false;
            }
        }

        private void Start()
        {
            m_GuestRoomPool = new ObjectPool(GuestRoomPrefabs, 4, this.transform);
            m_CultivationPool = new ObjectPool(CultivationPrefabs, 4, this.transform);
            m_EducationPool = new ObjectPool(EducationPrefabs, 4, this.transform);
            m_PreservationPool = new ObjectPool(PreservationPrefabs, 4, this.transform);
            m_StoragePool = new ObjectPool(StoragePrefabs, 4, this.transform);

            bubbles = FindObjectsOfType<InGameBubble.Bubble>();


            SpawnTrain(Vehicles.GUESTROOM, true);
            SpawnTrain(Vehicles.CULTIVATION, true);

        }
        float LastTrainTailPosition()
        {
            bubbles = FindObjectsOfType<Bubble.Bubble>();
            float Shortest = bubbles[0].transform.position.x;
            for (int i = 0; i < bubbles.Length; i++)
            {
                if (Shortest > bubbles[i].transform.position.x)
                    Shortest = bubbles[i].transform.position.x;
            }
            return Shortest;
        }
        public void SpawnTrain(Vehicles vehicles)
        {
            arrowScrollers[0].ExpendLimitMinValue(ExpendMinAmount);
            arrowScrollers[0].ExpendLimitMaxValue(ExpendMaxAmount);

            arrowScrollers[1].ExpendLimitMinValue(ExpendMinAmount);
            arrowScrollers[1].ExpendLimitMaxValue(ExpendMaxAmount);

            IncreaseReourceInTrain(vehicles);

            GameObject pool = InstantiateTrain(vehicles);
            float lastTail = LastTrainTailPosition();
            pool.transform.position = new Vector3(lastTail + SpawnSpacing, InitPos.position.y, InitPos.position.z);
        }
        public void SpawnTrain(Vehicles vehicles, bool IsInit)
        {
            if (!IsInit)
            {
                arrowScrollers[0].ExpendLimitMinValue(ExpendMinAmount);
                arrowScrollers[0].ExpendLimitMaxValue(ExpendMaxAmount);

                arrowScrollers[1].ExpendLimitMinValue(ExpendMinAmount);
                arrowScrollers[1].ExpendLimitMaxValue(ExpendMaxAmount);
            }

            GameObject pool = InstantiateTrain(vehicles);
            float lastTail = LastTrainTailPosition();
            pool.transform.position = new Vector3(lastTail + SpawnSpacing, InitPos.position.y, InitPos.position.z);
        }

        /// <summary>
        /// 열차에 따른 자원 지금
        /// </summary>
        /// <param name="vehicles"></param>
        public void IncreaseReourceInTrain(Vehicles vehicles)
        {
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    GameEvent.Instance.GetResource.ApplyMaxPopulation(1);
                    break;
                case Vehicles.CULTIVATION:
                    GameEvent.Instance.GetResource.ApplyMaxFood(1);
                    break;
                case Vehicles.EDUCATION:
                    GameEvent.Instance.GetResource.ApplyMaxLeaderShip(1);
                    break;
                default:
                    break;
            }
        }
        public GameObject InstantiateTrain(Vehicles vehicles)
        {
            GameObject @object = null;
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    @object = m_GuestRoomPool.pop();
                    ApplyGuestRoomAmount(1);
                    break;
                case Vehicles.CULTIVATION:
                    @object = m_CultivationPool.pop();
                    ApplyCultivationAmount(1);
                    break;
                case Vehicles.EDUCATION:
                    @object = m_EducationPool.pop();
                    ApplyEducationAmount(1);
                    break;
                case Vehicles.PRESERVE:
                    @object = m_PreservationPool.pop();
                    ApplyPreserveAmount(1);
                    break;
                case Vehicles.STORAGE:
                    @object = m_StoragePool.pop();
                    ApplyStorageAmount(1);
                    break;
                default:
                    break;
            }
            return @object;
        }

        [ContextMenu("TEST_SPAWN_GUESTROOM")]
        void TEST_SPAWN_GUESTROOM()
        {
            SpawnTrain(Vehicles.GUESTROOM);
        }

        [ContextMenu("TEST_SPAWN_CULTIVATION")]
        void TEST_SPAWN_CULTIVATION()
        {
            SpawnTrain(Vehicles.CULTIVATION);
        }

        [ContextMenu("TEST_SPAWN_EDUCATION")]
        void TEST_SPAWN_EDUCATION()
        {
            SpawnTrain(Vehicles.EDUCATION);
        }

        [ContextMenu("TEST_SPAWN_PRESERVE")]
        void TEST_SPAWN_PRESERVE()
        {
            SpawnTrain(Vehicles.PRESERVE);
        }

        [ContextMenu("TEST_SPAWN_STORAGE")]
        void TEST_SPAWN_STORAGE()
        {
            SpawnTrain(Vehicles.STORAGE);
        }
    }
}