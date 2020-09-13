using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Bubble
{
    public interface IBubble
    {
        void _BubbleClicked();
    }
    public class Bubble : MonoBehaviour
    {
        [SerializeField] Vehicles vehicles = Vehicles.GUESTROOM;
        public Vehicles GetVehicles() => vehicles;

        private BubbleSystem bubbleSystem;
        public int lastBubbleTime = 0;

        private GameObject PoolObject;
        bool IsCompleteTimer(float currentTime, float TargetTime)
        {
            if (currentTime >= TargetTime)
                return true;
            return false;
        }
        private void Start()
        {
            bubbleSystem = BubbleSystem.Instance;
            GameEvent.Instance.SubscribeBubbleEvent(() => { BubbleTiming(); });
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    PoolObject = bubbleSystem.m_PopulationPool.pop();
                    break;
                case Vehicles.CULTIVATION:
                    PoolObject = bubbleSystem.m_FoodPool.pop();
                    break;
                case Vehicles.EDUCATION:
                    PoolObject = bubbleSystem.m_LeaderShipPool.pop();
                    break;
                default:
                    break;
            }
            PoolObject.transform.position = bubbleSystem.ConvertWorldToScreenPoint(transform.position);
            BubbleSystem.SetActive(PoolObject, true);
            PoolObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                    lastBubbleTime = 0;
                    IncreaseResourceInBubble(vehicles);
                    PoolObject.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                });
        }

        bool IsReward = true;
        public void BubbleTiming()
        {
            lastBubbleTime++;
            if (IsCompleteTimer(lastBubbleTime, bubbleSystem.BubbleEndTime))
            {
                BubbleSystem.SetActive(PoolObject, false);
                if (IsReward)
                {
                    IncreaseResourceInBubble(vehicles);
                    lastBubbleTime = 0;
                    IsReward = false;
                }
            }
            if (IsCompleteTimer(lastBubbleTime, bubbleSystem.BubbleTables[(int)vehicles].bubbleUpTime))
            {
                BubbleSystem.SetActive(PoolObject, true);
                lastBubbleTime = 0;
                IsReward = true;
            }
        }

        /// <summary>
        /// 기차칸 종류에 따른 자원 배부 함수
        /// </summary>
        /// <param name="vehicles">기차칸</param>
        protected void IncreaseResourceInBubble(Vehicles vehicles)
        {
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    GameEvent.Instance.GetResource.ApplyPopulation(1);

                    break;
                case Vehicles.CULTIVATION:
                    GameEvent.Instance.GetResource.ApplyFood(1);

                    break;
                case Vehicles.EDUCATION:
                    GameEvent.Instance.GetResource.ApplyLeaderShip(1);
                    break;
                default:
                    break;
            }
        }
    }
}