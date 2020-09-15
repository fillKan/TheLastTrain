﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame.INPUT.Scroll;
using InGameBubble = InGame.Bubble;

namespace InGame.Train
{
    public class Train : MonoBehaviour
    {
        [SerializeField] private TrainScroller trainScroller;

        [SerializeField] private Transform InitPos;
        [SerializeField] private GameObject GuestRoomPrefabs;
        [SerializeField] private GameObject CultivationPrefabs;
        [SerializeField] private GameObject EducationPrefabs;

        private objectPool m_GuestRoomPool;
        private objectPool m_CultivationPool;
        private objectPool m_EducationPool;

        public List<GameObject> Trains;

        [SerializeField] float SpawnSpacing = -4;
        [SerializeField] float ExpendMinAmount = -2.0f;
        [SerializeField] float ExpendMaxAmount = 2.0f;

        InGameBubble.Bubble[] bubbles;
        private void Start()
        {
            m_GuestRoomPool = new objectPool(GuestRoomPrefabs, 4, this.transform);
            m_CultivationPool = new objectPool(CultivationPrefabs, 4, this.transform);
            m_EducationPool = new objectPool(EducationPrefabs, 4, this.transform);

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
            trainScroller.ExpendLimitMinValue(ExpendMinAmount);
            trainScroller.ExpendLimitMaxValue(ExpendMaxAmount);
            GameObject pool = InstantiateTrain(vehicles);
            float lastTail = LastTrainTailPosition();
            pool.transform.position = new Vector3(lastTail + SpawnSpacing, InitPos.position.y, InitPos.position.z);
            Trains.Add(pool);
        }
        public void SpawnTrain(Vehicles vehicles, bool IsInit)
        {
            if (!IsInit)
            {
                trainScroller.ExpendLimitMinValue(ExpendMinAmount);
                trainScroller.ExpendLimitMaxValue(ExpendMaxAmount);
            }
            GameObject pool = InstantiateTrain(vehicles);
            float lastTail = LastTrainTailPosition();
            pool.transform.position = new Vector3(lastTail + SpawnSpacing, InitPos.position.y, InitPos.position.z);
            Trains.Add(pool);
        }
        public GameObject InstantiateTrain(Vehicles vehicles)
        {
            GameObject @object = null;
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    @object = m_GuestRoomPool.pop();
                    break;
                case Vehicles.CULTIVATION:
                    @object = m_CultivationPool.pop();
                    break;
                case Vehicles.EDUCATION:
                    @object = m_EducationPool.pop();
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
    }
}