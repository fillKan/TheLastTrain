using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InGame.Bubble;

using Random = UnityEngine.Random;

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
        None,
    }

    public class EventEffect : MonoBehaviour
    {
        public List<EventTable> eventTable;

        public Image Icon = null;
        public Text EvtNameText = null;
        public Text EvtDescriptionText = null;

        public GameObject EventUI;

        GameEvent evt;
        List<EventID> OnResourceEvt;
        List<EventID> OnSpecialEvt;
        List<EventID> OnNormalEvt;

        void Start()
        {
            OnResourceEvt = new List<EventID>();
            OnSpecialEvt = new List<EventID>();
            OnNormalEvt = new List<EventID>();
            evt = GameEvent.Instance;

            EventUI.SetActive(false);

            eventTable.ForEach(table =>
            {
                table.IsEventOn = false;
            });
        }

        public void ApplyEventUI(EventTable table)
        {
            Icon.sprite = table.icon;
            EvtNameText.text = table.name;
            EvtDescriptionText.text = table.description;
        }
        public static GameEvent GetEvent() => GameEvent.Instance;
        public static UI.Resource.Resource GetResource() => GetEvent().GetResource;
        public static ResourceTable GetResourceTable() => GetResource().GetResourceTable;

        private EventTable resultTable;
        public static EventTable lastestResultTable = null;
        

        public void ApplyEffect()
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;

            OnResourceEvt.Clear();
            OnSpecialEvt.Clear();
            OnNormalEvt.Clear();
            resultTable = null;

            EventUI.SetActive(true);

            // 이벤트 리스트 검사
            eventTable.ForEach(table =>
            {
                table.IsEventOn = false;

                EventID eventID = ResourceCondition(table, table.ID, GetEvent());
                if (eventID != EventID.None)
                    OnResourceEvt.Add(eventID);


                eventID = NormalCondition(table, table.ID, GetEvent());
                if (eventID != EventID.None)
                    OnNormalEvt.Add(eventID);


                eventID = SpecialCondition(table, table.ID, GetEvent());
                if (eventID != EventID.None)
                    OnSpecialEvt.Add(eventID);


            });

            if (OnResourceEvt.Count > 0)
            {
                int i = Random.Range(0, OnResourceEvt.Count);
                if (eventTable[(int)OnResourceEvt[i]].IsEventOn == true)
                {
                    resultTable = eventTable[(int)OnResourceEvt[i]];
                    ApplyEventUI(resultTable);
                }
            }
            else
            {
                
                bool IsCompare = (Random.Range(0, 2) == 0) ? true : false;
                if (IsCompare)
                {
                    if (OnNormalEvt.Count > 0)
                    {
                        int i = Random.Range(0, OnNormalEvt.Count);
                        if (eventTable[(int)OnNormalEvt[i]].IsEventOn == true)
                        {
                            resultTable = eventTable[(int)OnNormalEvt[i]];
                            ApplyEventUI(resultTable);
                        }
                    }
                }
                else
                {
                    if (OnSpecialEvt.Count > 0)
                    {
                        int i = Random.Range(0, OnSpecialEvt.Count);
                        if (eventTable[(int)OnSpecialEvt[i]].IsEventOn == true)
                        {
                            resultTable = eventTable[(int)OnSpecialEvt[i]];
                            ApplyEventUI(resultTable);
                        }
                    }
                    else
                    {
                        if (OnNormalEvt.Count > 0)
                        {
                            int i = Random.Range(0, OnNormalEvt.Count);
                            if (eventTable[(int)OnNormalEvt[i]].IsEventOn == true)
                            {
                                resultTable = eventTable[(int)OnNormalEvt[i]];
                                ApplyEventUI(resultTable);
                            }
                        }
                    }
                }


            }

            if (lastestResultTable != resultTable)
            {
                if (lastestResultTable != null)
                {
                    switch (lastestResultTable.ID)
                    {
                        case EventID.NO2:
                            evt.GetWeek.OnThreeDayEvent = null;
                            break;
                        case EventID.NO9:
                            GameEvent.Instance.WeekUploadTime = 1.0f;
                            break;
                        case EventID.NO24:
                            evt.GetWeek.OnThreeDayEvent = null;
                            evt.GetWeek.OnTwoDayEvent = null;
                            break;
                    }
                }
                lastestResultTable = resultTable;
            }

            GameEvent.Pause();
        }

        public void _OnClickExit()
        {
            GameEvent.Pause();
            if (resultTable != null)
            {
                switch (resultTable.tagType)
                {
                    case TAG.NORMAL:
                        NormalEvts(resultTable, resultTable.ID, evt);
                        break;
                    case TAG.RESOURCE:
                        ResourceEvts(resultTable, resultTable.ID, evt);
                        break;
                    case TAG.SPECIAL:
                        SpecialEvts(resultTable, resultTable.ID, evt);
                        break;
                    default:
                        break;
                }
            }
        }

        EventID ResourceCondition(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            switch (_eventId)
            {
                case EventID.NO1:     // 인구 급증
                    if (resourceTable.populationTable.Now > resourceTable.populationTable.Max)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO2:     // 식중독 발생
                    if (resourceTable.foodTable.Now > resourceTable.foodTable.Max)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                default:
                    break;
            }
            return EventID.None;
        }

        EventID NormalCondition(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;

            switch (_eventId)
            {
                case EventID.NO3:     // 단풍잎 이야기
                case EventID.NO4:     // 식량 배분
                case EventID.NO5:     // 비상식량 발견
                case EventID.NO6:     // 사고 발생
                case EventID.NO7:         // 톱니바퀴 개발
                case EventID.NO8:         // 감독관 배치
                    table.IsEventOn = true;
                    return _eventId;
                case EventID.NO9:         // 장비를 정지합니다
                    if ((resource.GetResourceTable.populationTable.Now
                        <= (resource.GetResourceTable.populationTable.Max / 2))
                        && evt.switchCondition.IsSwitchState(SwitchID.NO1) == false)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO10:        // 부정 부패
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO3) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO11:        // 노후 부품 발견
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO1) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO12:        // 식량 분쟁
                    if ((resource.GetResourceTable.populationTable.Now
                        - resource.GetResourceTable.foodTable.Now) > 2)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO13:        // 식량 창고 개선
                    if (resource.GetResourceTable.foodTable.Now
                        >= (resource.GetResourceTable.foodTable.Max - 2))
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO14:        // 어이쿠 손이 미끄러졌네
                    if (resource.GetResourceTable.foodTable.Now > 3)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO15:        // 재배 시설 강화
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO1) == true
                        && evt.switchCondition.IsSwitchState(SwitchID.NO3) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO16:        //베이비 붐 시대
                    if (evt.GetTrain.GetTrainAmount().GuestRoom >= 2)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO17:        // 의문의 지도자
                    if (PolicyHub.Instance.GetPolicy.Count >= 1)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                default:
                    break;
            }
            return EventID.None;
        }

        EventID SpecialCondition(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            switch (_eventId)
            {
                case EventID.NO18:        // 식인 사건
                    if (resourceTable.foodTable.Now < (resourceTable.foodTable.Now / 2)
                        && evt.switchCondition.IsSwitchState(SwitchID.NO5) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO19:        // 인신 공양
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO5) == true
                        && evt.switchCondition.IsSwitchState(SwitchID.NO6) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO20:        // 반란 발생
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO7) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO21:        // 집단 시위
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO6) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO22:        // 알 수 없는 종교
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO4) == true
                        && evt.switchCondition.IsSwitchState(SwitchID.NO5) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO23:        // 만족하는 복지
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO8) == true)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO24:        // 만성 피로
                    float result = resource.GetResourceTable.populationTable.Max * 0.6f;
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO9) == true
                        && resource.GetResourceTable.populationTable.Now > result)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                default:
                    break;
            }
            return EventID.None;
        }

        void ResourceEvts(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;

            switch (_eventId)
            {
                case EventID.NO1:     // 인구 급증
                    resource.ApplyLeaderShip(-2);
                    ApplyEventUI(table);
                    break;
                case EventID.NO2:     // 식중독 발생
                    resource.ApplyLeaderShip(-3);
                    ApplyEventUI(table);
                    evt.SubscribeThreeDayEvent(() => { resource.ApplyPopulation(-1); });
                    table._nextEvent = false;
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
                case EventID.NO3:     // 단풍잎 이야기
                    resource.ApplyLeaderShip(1);
                    ApplyEventUI(table);
                    break;
                case EventID.NO4:     // 식량 배분
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO5:     // 비상식량 발견
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO6:     // 사고 발생
                    resource.ApplyPopulation(-(int)resource.GetPopulationResource(0.1d));
                    evt.switchCondition.SwitchON(SwitchID.NO2);
                    ApplyEventUI(table);
                    break;
                case EventID.NO7:         // 톱니바퀴 개발
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    evt.switchCondition.SwitchON(SwitchID.NO1);
                    ApplyEventUI(table);
                    break;
                case EventID.NO8:         // 감독관 배치
                    resource.ApplyLeaderShip(1);
                    evt.switchCondition.SwitchON(SwitchID.NO3);
                    ApplyEventUI(table);
                    break;
                case EventID.NO9:         // 장비를 정지합니다
                    GameEvent.Instance.WeekUploadTime = 2.0f;
                    ApplyEventUI(table);
                    break;
                case EventID.NO10:        // 부정 부패
                    resource.ApplyFood(-(int)resource.GetFoodResource(0.2d));
                    resource.ApplyLeaderShip(-2);
                    ApplyEventUI(table);
                    break;
                case EventID.NO11:        // 노후 부품 발견
                    evt.switchCondition.SwitchOff(SwitchID.NO1);
                    ApplyEventUI(table);
                    break;
                case EventID.NO12:        // 식량 분쟁
                    resource.ApplyFood(-(int)resource.GetFoodResource(0.3d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO13:        // 식량 창고 개선
                    resource.ApplyMaxFood(1);
                    resource.ApplyPopulation(-(int)resource.GetFoodResource(0.2d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO14:        // 어이쿠 손이 미끄러졌네
                    resource.ApplyFood(-(int)resource.GetFoodResource(0.3d));
                    resource.ApplyLeaderShip(-1);
                    ApplyEventUI(table);
                    break;
                case EventID.NO15:        // 재배 시설 강화
                    resource.ApplyPopulation((int)resource.GetFoodResource(0.2d));
                    resource.ApplyFood((int)resource.GetFoodResource(0.3d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO16:        //베이비 붐 시대
                    resource.ApplyPopulation((int)((double)evt.GetTrain.GetTrainAmount().GuestRoom * resource.GetFoodResource(0.1d)));
                    ApplyEventUI(table);
                    break;
                case EventID.NO17:        // 의문의 지도자
                    resource.ApplyLeaderShip(-1);
                    resource.ApplyFood((int)resource.GetFoodResource(0.2d));
                    evt.switchCondition.SwitchON(SwitchID.NO4);
                    ApplyEventUI(table);
                    break;
                default:
                    break;
            }

        }
        void SpecialEvts(EventTable table, EventID _eventId, GameEvent evt)
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;
            switch (_eventId)
            {
                case EventID.NO18:        // 식인 사건
                    resource.ApplyPopulation(-(int)resource.GetPopulationResource(0.3d));
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    resource.ApplyLeaderShip(-4);
                    ApplyEventUI(table);
                    break;
                case EventID.NO19:        // 인신 공양
                    resource.ApplyPopulation(-(int)resource.GetPopulationResource(0.5d));
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    resource.ApplyLeaderShip(-4);
                    ApplyEventUI(table);
                    break;
                case EventID.NO20:        // 반란 발생
                    SpecialBubble.Instance.SpawnSpecialBubble(SpecialBubbleType.REBELLION);
                    ApplyEventUI(table);
                    break;
                case EventID.NO21:        // 집단 시위
                    SpecialBubble.Instance.SpawnSpecialBubble(SpecialBubbleType.DEMONSTRATE);
                    ApplyEventUI(table);
                    break;
                case EventID.NO22:        // 알 수 없는 종교
                    resource.ApplyLeaderShip(-1);
                    SpecialBubble.Instance.SpawnSpecialBubble(SpecialBubbleType.FALSE_RELIGION);
                    ApplyEventUI(table);
                    break;
                case EventID.NO23:        // 만족하는 복지
                    resource.ApplyLeaderShip(2);
                    ApplyEventUI(table);
                    break;
                case EventID.NO24:        // 만성 피로
                    table._nextEvent = false;
                    evt.SubscribeThreeDayEvent(() => { resource.ApplyLeaderShip(-1); });
                    evt.SubscribeTwoDayEvent(() => { resource.ApplyPopulation(-1); });
                    ApplyEventUI(table);
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