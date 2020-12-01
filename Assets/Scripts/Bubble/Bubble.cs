using System.Collections;
using UnityEngine;
using InGame.UI.Resource;

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
        private GameEvent gameEvent;

        private Resource resource;

        public int lastBubbleTime = 0;

        private GameObject PoolObject;

        private bool IsReward { get; set; } = true;

        void Reward()
        {
            IncreaseResourceInBubble(vehicles);
            lastBubbleTime = 0;
            
            IsReward = false;

            SoundSystem.Instance.PlayOneShot("BubbleTouch");
        }
        

        bool IsCompleteTimer(float currentTime, float TargetTime)
        {
            if (currentTime >= TargetTime)
                return true;
            return false;
        }
        private GameObject popGameObjectInPool(Vehicles vehicles)
        {
            GameObject @object = null;
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    @object = bubbleSystem.m_PopulationPool.pop();
                    break;
                case Vehicles.CULTIVATION:
                    @object = bubbleSystem.m_FoodPool.pop();
                    break;
                case Vehicles.EDUCATION:
                    @object = bubbleSystem.m_LeaderShipPool.pop();
                    break;
                default:
                    break;
            }
            return @object;
        }
        private void pushGameObjectInPool(Vehicles vehicles, GameObject pushObject)
        {
            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    bubbleSystem.m_PopulationPool.push(pushObject);
                    break;
                case Vehicles.CULTIVATION:
                    bubbleSystem.m_FoodPool.push(pushObject);
                    break;
                case Vehicles.EDUCATION:
                    bubbleSystem.m_LeaderShipPool.push(pushObject);
                    break;
                default:
                    break;
            }
        }
        private void Start()
        {
            bubbleSystem = BubbleSystem.Instance;
            gameEvent = GameEvent.Instance;

            resource = gameEvent.GetResource;

            StartCoroutine(EInit());
        }
        IEnumerator EInit()
        {
            if (this.vehicles == Vehicles.STORAGE) yield break;

            yield return new WaitForSeconds(bubbleSystem.BubbleTables[(int)vehicles].bubbleUpTime);

            PoolObject = popGameObjectInPool(vehicles);
            PoolObject.transform.position = BubbleSystem.ConvertWorldToScreenPoint(transform.position);
            PoolObject.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            PoolObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                Reward();
                pushGameObjectInPool(vehicles, PoolObject);
            });
            StartCoroutine(EUpdate());

            gameEvent.SubscribeBubbleEvent(() => { BubbleTiming(); });
            
        }
        IEnumerator EUpdate()
        {
            while (true)
            {
                PoolObject.transform.position
                        = BubbleSystem.ConvertWorldToScreenPoint(transform.position);
                yield return null;
            }
        }
        public void BubbleTiming()
        {
            if (bubbleSystem.IsAbleProduce)
            {
                lastBubbleTime++;
                if (IsCompleteTimer(lastBubbleTime, bubbleSystem.BubbleEndTime))
                {
                    BubbleSystem.SetActive(PoolObject, false);
                    if (IsReward)
                    {
                        Reward();
                        pushGameObjectInPool(vehicles, PoolObject);
                    }
                }
                if (IsCompleteTimer(lastBubbleTime, bubbleSystem.BubbleTables[(int)vehicles].bubbleUpTime))
                {
                    PoolObject = popGameObjectInPool(vehicles);
                    PoolObject.transform.position = BubbleSystem.ConvertWorldToScreenPoint(transform.position);
                    PoolObject.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
                    PoolObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                    {
                        Reward();
                        pushGameObjectInPool(vehicles, PoolObject);
                    });
                    BubbleSystem.SetActive(PoolObject, true);
                    lastBubbleTime = 0;
                    IsReward = true;
                    SoundSystem.Instance.PlayOneShot("BubbleUp");
                }
            }
        }

        /// <summary>
        /// 기차칸 종류에 따른 자원 배부 함수
        /// </summary>
        /// <param name="vehicles">기차칸</param>
        protected void IncreaseResourceInBubble(Vehicles vehicles)
        {
            const int _unitAmount = 1;

            switch (vehicles)
            {
                case Vehicles.GUESTROOM:
                    resource.ApplyPopulation(_unitAmount);
                    break;
                case Vehicles.CULTIVATION:
                    resource.ApplyFood(_unitAmount);
                    break;
                case Vehicles.EDUCATION:
                    resource.ApplyLeaderShip(_unitAmount);
                    break;
                default:
                    break;
            }
        }
    }
}