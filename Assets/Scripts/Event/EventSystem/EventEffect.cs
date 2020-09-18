using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Event
{
    public enum EventID
    {
        NO1,
        NO2,
        NO3,
        NO4,
        NO5,
        NO6,
        NO7,
        NO8,
        NO9,
        NO10,
        NO11,
        NO12,
        NO13,
        NO14,
        NO15,
        NO16,
        NO17,
        NO18,
        NO19,
        NO20,
        NO21,
        NO22,
        NO23,
        NO24,
    }

    public class EventEffect : MonoBehaviour
    {
        public List<EventTable> eventTable;

        bool IsResourceEventOn = true;
        bool IsNormalEventOn = true;
        bool IsSpecialEventOn = true;
        

        public void ApplyEffect()
        {
            GameEvent evt = GameEvent.Instance;
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            

            EventID choice = EventID.NO1;
            eventTable.ForEach(table => {
                table._nextEvent = true;
                if (IsResourceEventOn)
                {
                    if (table.IsEventOn && table.tagType == TAG.RESOURCE)
                    {
                        choice = (EventID)Random.Range(0, 2);
                        ResourceEvts(table, choice, evt);
                    }
                }
                else if (IsSpecialEventOn)
                {
                    if (table.IsEventOn && table.tagType == TAG.SPECIAL)
                    {
                        choice = (EventID)Random.Range(17, 24);
                        SpecialEvts(table, choice, evt);
                    }
                }
                else if (IsNormalEventOn)
                {
                    if (table.IsEventOn && table.tagType == TAG.NORMAL)
                    {
                        choice = (EventID)Random.Range(2, 17);
                        NormalEvts(table, choice, evt);
                    }
                }
            });
        }
        void ResourceEvts(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            switch (_eventId)
            {
                case EventID.NO1:     // 인구 급증
                    if (resourceTable.populationTable.Now > resourceTable.populationTable.Max)
                    {

                        Debug.Log(table.name);
                        resource.ApplyLeaderShip(-1);
                    }
                    break;
                case EventID.NO2:     // 식중독 발생
                    if (resourceTable.foodTable.Now > resourceTable.foodTable.Max)
                    {
                        Debug.Log(table.name);
                        resource.ApplyLeaderShip(-2);
                        table._nextEvent = false;
                    }
                    break;
                default:
                    break;
            }

            if (!table._nextEvent && _eventId == EventID.NO2)
                evt.SubscribeThreeDayEvent(() => { resource.ApplyPopulation(-1); });
            else
                evt.DescribeThreeDayEvent(() => { resource.ApplyPopulation(-1); });

        }
        void SpecialEvts(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            switch (_eventId)
            {
                case EventID.NO3:     // 단풍잎 이야기
                    Debug.Log(table.name);
                    resource.ApplyLeaderShip(1);
                    break;
                case EventID.NO4:     // 식량 배분
                    resource.ApplyFood(1);
                    break;
                case EventID.NO5:     // 비상식량 발견
                    resource.ApplyFood(1);
                    break;
                case EventID.NO6:     // 사고 발생
                    resource.ApplyFood(-1);
                    evt.switchCondition.SwitchON(SwitchID.NO2);
                    break;
                case EventID.NO7:         // 톱니바퀴 개발
                    resource.ApplyFood(1);
                    evt.switchCondition.SwitchON(SwitchID.NO1);
                    break;
                case EventID.NO8:         // 감독관 배치
                    resource.ApplyLeaderShip(1);
                    evt.switchCondition.SwitchON(SwitchID.NO3);
                    break;
                case EventID.NO9:         // 장비를 정지합니다
                    if ((resource.GetResourceTable.populationTable.Now 
                        <= (resource.GetResourceTable.populationTable.Max / 2))
                        && evt.switchCondition.IsSwitchState(SwitchID.NO1) == false)
                    {

                    }
                    break;
                case EventID.NO10:        // 부정 부패
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO3) == true)
                    {
                        resource.ApplyFood(-1);
                        resource.ApplyLeaderShip(-1);
                    }
                    break;
                case EventID.NO11:        // 노후 부품 발견
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO1) == true)
                        evt.switchCondition.SwitchOff(SwitchID.NO1);
                    break;
                case EventID.NO12:        // 식량 분쟁
                    if ((resource.GetResourceTable.populationTable.Now
                        - resource.GetResourceTable.foodTable.Now) > 2)
                    {
                        resource.ApplyFood(-2);
                    }
                    break;
                case EventID.NO13:        // 식량 창고 개선
                    if (resource.GetResourceTable.foodTable.Now 
                        >= (resource.GetResourceTable.foodTable.Max - 2))
                    {
                        resource.ApplyMaxFood(1);
                        resource.ApplyPopulation(-2);
                    }
                    break;
                case EventID.NO14:        // 어이쿠 손이 미끄러졌네
                    if (resource.GetResourceTable.foodTable.Now > 3)
                    {
                        resource.ApplyFood(-2);
                        resource.ApplyLeaderShip(-1);
                    }
                    break;
                case EventID.NO15:        // 재배 시설 강화
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO1) == true
                        && evt.switchCondition.IsSwitchState(SwitchID.NO3) == true)
                    {
                        resource.ApplyPopulation(2);
                        resource.ApplyFood(1);
                    }
                    break;
                case EventID.NO16:        //베이비 붐 시대
                    if (evt.GetTrain.GetTrainAmount().GuestRoom >= 2)
                    {
                        resource.ApplyPopulation(System.Convert.ToInt32(evt.GetTrain.GetTrainAmount().GuestRoom));
                    }
                    break;
                case EventID.NO17:        // 의문의 지도자
                    if (PolicyHub.Instance.GetPolicy.Count >= 1)
                    {
                        resource.ApplyLeaderShip(-1);
                        resource.ApplyFood(2);
                        evt.switchCondition.SwitchON(SwitchID.NO4);
                    }
                    break;
                default:
                    break;
            }

        }
        void NormalEvts(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            switch (_eventId)
            {
                case EventID.NO18:        // 식인 사건
                    resource.ApplyPopulation(-2);
                    resource.ApplyFood(1);
                    resource.ApplyLeaderShip(-4);
                    break;
                case EventID.NO19:        // 인신 공양
                    resource.ApplyPopulation(-3);
                    double value = System.Convert.ToDouble(resource.GetResourceTable.foodTable.Max) * 0.2d;
                    resource.ApplyFood(System.Convert.ToInt32(value));
                    resource.ApplyLeaderShip(-2);
                    break;
                case EventID.NO20:        // 반란 발생

                    break;
                case EventID.NO21:        // 집단 시위

                    break;
                case EventID.NO22:        // 알 수 없는 종교

                    break;
                case EventID.NO23:        // 만족하는 복지

                    break;
                case EventID.NO24:        // 만성 피로

                    break;
                default:
                    break;
            }

        }

        public void PrintAllTable()
        {
            eventTable.ForEach(table => {

                Debug.Log($"ID : {table.ID}");
                Debug.Log($"IsEvent On : {table.IsEventOn}");                               
                Debug.Log($"TAG : {table.tagType}");
                Debug.Log($"Name : {table.name}");
                Debug.Log($"sprite : {table.icon.name}");
                Debug.Log($"description : {table.description}");
                Debug.Log($"================================");
            });
        }
    }
}