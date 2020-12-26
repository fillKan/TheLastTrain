using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InGame.Bubble;

using Random = UnityEngine.Random;
namespace InGame.Event
{
    [Serializable]
    public enum EventID
    {
        /// <summary>
        /// 인구 급증 (RESOURCE)
        /// </summary>
        NO1,
        /// <summary>
        /// 식중독 발생 (RESOURCE)
        /// </summary>
        NO2,
        /// <summary>
        /// 단풍잎 이야기 (NORMAL)
        /// </summary>
        NO3,
        /// <summary>
        /// 식량 배분 (NORMAL)
        /// </summary>
        NO4,
        /// <summary>
        /// 비상식량 발견 (NORMAL)
        /// </summary>
        NO5,
        /// <summary>
        /// 사고 발생 (NORMAL)
        /// </summary>
        NO6,
        /// <summary>
        /// 톱니바퀴 개발 (NORMAL)
        /// </summary>
        NO7,
        /// <summary>
        /// 감독관 배치 (NORMAL)
        /// </summary>
        NO8,
        /// <summary>
        /// 장비를 정지합니다 (NORMAL)
        /// </summary>
        NO9,
        /// <summary>
        /// 부정 부패 (NORMAL)
        /// </summary>
        NO10,
        /// <summary>
        /// 노후 부품 발견 (NORMAL)
        /// </summary>
        NO11,
        /// <summary>
        /// 식량 분쟁 (NORMAL)
        /// </summary>
        NO12,
        /// <summary>
        /// 식량 창고 개선 (NORMAL)
        /// </summary>
        NO13,
        /// <summary>
        /// 어이쿠 손이 미끄러졌네 (NORMAL)
        /// </summary>
        NO14,
        /// <summary>
        /// 재배 시설 강화 (NORMAL)
        /// </summary>
        NO15,
        /// <summary>
        /// 베이비 붐 시대 (NORMAL)
        /// </summary>
        NO16,
        /// <summary>
        /// 의문의 지도자 (NORMAL)
        /// </summary>
        NO17,
        /// <summary>
        /// 식인 사건 (SPECIAL)
        /// </summary>
        NO18,
        /// <summary>
        /// 인신 공양 (SPECIAL)
        /// </summary>
        NO19,
        /// <summary>
        /// 반란 발생 (SPECIAL)
        /// </summary>
        NO20,
        /// <summary>
        /// 집단 시위 (SPECIAL)
        /// </summary>
        NO21,
        /// <summary>
        /// 알 수 없는 종교 (SPECIAL)
        /// </summary>
        NO22,
        /// <summary>
        /// 만족하는 복지 (SPECIAL)
        /// </summary>
        NO23,
        /// <summary>
        /// 만성 피로 (SPECIAL)
        /// </summary>
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
        private List<EventID> OnResourceEvt;
        private List<EventID> OnSpecialEvt;
        private List<EventID> OnNormalEvt;

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
        
        /// <summary>
        /// 이벤트 리스트 검사 : VOID
        /// </summary>
        private void EventInspection()
        {
            OnResourceEvt.Clear();
            OnSpecialEvt.Clear();
            OnNormalEvt.Clear();

            
            eventTable.ForEach(table =>
            {
                table._nextEvent = true;
                table.IsEventOn = false;

                EventID eventID = ResourceCondition(table, table.ID, GetEvent());
                if (eventID != EventID.None)
                {
                    OnResourceEvt.Add(eventID);
                }


                eventID = NormalCondition(table, table.ID, GetEvent());
                if (eventID != EventID.None)
                {
                    OnNormalEvt.Add(eventID);
                }


                eventID = SpecialCondition(table, table.ID, GetEvent());
                if (eventID != EventID.None)
                {
                    OnSpecialEvt.Add(eventID);
                }
            });
        }

        /// <summary>
        /// 이벤트 산출 : VOID
        /// </summary>
        private void EventProduction(bool isInspectionSpeical = true)
        {
            if (OnSpecialEvt.Count > 0 && isInspectionSpeical)
            {
                int i = Random.Range(0, OnSpecialEvt.Count);
                if (eventTable[(int)OnSpecialEvt[i]].IsEventOn == true)
                {
                    resultTable = eventTable[(int)OnSpecialEvt[i]];
                    ApplyEventUI(resultTable);
                }
            }
            else // 특수 이벤트 및 일반 이벤트 On off 검사
            {

                bool IsCompare = (Random.Range(0, 2) == 0) ? true : false;
                //Debug.Log(OnSpecialEvt.Count + " : " + IsCompare);
                if (IsCompare)
                {
                    if (OnResourceEvt.Count > 0)        // 자원 이벤트가 On인가 (하나라도 충족되는 조건이 있나)
                    {
                        int i = Random.Range(0, OnResourceEvt.Count);
                        if (eventTable[(int)OnResourceEvt[i]].IsEventOn == true) // 자원 이벤트 On Off 검사
                        {
                            resultTable = eventTable[(int)OnResourceEvt[i]];        // 자원 이벤트 중 하나 선택
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
                    else
                    {
                        if (OnResourceEvt.Count > 0)        // 자원 이벤트가 On인가 (하나라도 충족되는 조건이 있나)
                        {
                            int i = Random.Range(0, OnResourceEvt.Count);
                            if (eventTable[(int)OnResourceEvt[i]].IsEventOn == true) // 자원 이벤트 On Off 검사
                            {
                                resultTable = eventTable[(int)OnResourceEvt[i]];        // 자원 이벤트 중 하나 선택
                                ApplyEventUI(resultTable);
                            }
                        }
                    }
                }


            }
        }

        public void ApplyEffect()
        {
            StartCoroutine(EEventTableInspection());
        }

        IEnumerator EEventTableInspection()
        {
            UI.Resource.Resource resource = evt.GetResource;
            ResourceTable resourceTable = resource.GetResourceTable;

            resultTable = null;
            EventUI.SetActive(true);
            evt.switchCondition.SwitchCheck();


            EventInspection();
            EventProduction();



            //if (OnResourceEvt.Count > 0)        // 자원 이벤트가 On인가 (하나라도 충족되는 조건이 있나)
            //{
            //    int i = Random.Range(0, OnResourceEvt.Count);
            //    if (eventTable[(int)OnResourceEvt[i]].IsEventOn == true) // 자원 이벤트 On Off 검사
            //    {
            //        resultTable = eventTable[(int)OnResourceEvt[i]];        // 자원 이벤트 중 하나 선택
            //        ApplyEventUI(resultTable);
            //    }
            //}
            //else // 특수 이벤트 및 일반 이벤트 On off 검사
            //{

            //    bool IsCompare = (Random.Range(0, 2) == 0) ? true : false;
            //    //Debug.Log(OnSpecialEvt.Count + " : " + IsCompare);
            //    if (IsCompare)
            //    {
            //        if (OnNormalEvt.Count > 0)
            //        {
            //            int i = Random.Range(0, OnNormalEvt.Count);
            //            if (eventTable[(int)OnNormalEvt[i]].IsEventOn == true)
            //            {
            //                resultTable = eventTable[(int)OnNormalEvt[i]];
            //                ApplyEventUI(resultTable);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (OnSpecialEvt.Count > 0)
            //        {
            //            int i = Random.Range(0, OnSpecialEvt.Count);
            //            if (eventTable[(int)OnSpecialEvt[i]].IsEventOn == true)
            //            {
            //                resultTable = eventTable[(int)OnSpecialEvt[i]];
            //                ApplyEventUI(resultTable);
            //            }
            //        }
            //        else
            //        {
            //            if (OnNormalEvt.Count > 0)
            //            {
            //                int i = Random.Range(0, OnNormalEvt.Count);
            //                if (eventTable[(int)OnNormalEvt[i]].IsEventOn == true)
            //                {
            //                    resultTable = eventTable[(int)OnNormalEvt[i]];
            //                    ApplyEventUI(resultTable);
            //                }
            //            }
            //        }
            //    }


            //}

            if (lastestResultTable == resultTable)
            {
                EventInspection();
                EventProduction(false);
            }


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

            eventTable.ForEach(e => e.IsEventOn = false);
            GameEvent.Pause();

            yield return null;
        }

        // Call this Method, If Click Exit Button in Game
        public void _OnClickExit()
        {
            if (resultTable != null)
            {
                GameEvent.Pause();
                //Debug.Log($"{resultTable.tagType} : {resultTable.name} : {resultTable.ID}");
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
                        && evt.switchCondition.IsSwitchState(SwitchID.NO1) == true)
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
                    if (((float)resource.GetResourceTable.foodTable.Now / resource.GetResourceTable.foodTable.Max)
                        >= 0.2f)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO14:        // 어이쿠 손이 미끄러졌네
                    if (resource.GetResourceTable.foodTable.Now > resource.GetResourceTable.foodTable.Max * 0.3f)
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
                    if (evt.GetTrain.GetTrainAmount().GuestRoom >= (evt.GetWeek.GetAccumulateDate / 150f) * 2)
                    {
                        table.IsEventOn = true;
                        return _eventId;
                    }
                    break;
                case EventID.NO17:        // 의문의 지도자
                    if (PolicySystem.Instance.GetAccumulatePolicy.Count >= 1)
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
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO5)
                        && evt.switchCondition.IsSwitchState(SwitchID.NO6))
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
                    if (evt.switchCondition.IsSwitchState(SwitchID.NO4)
                        && evt.switchCondition.IsSwitchState(SwitchID.NO5))
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
            uint accumulateDate = evt.GetWeek.GetAccumulateDate;

            switch (_eventId)
            {
                case EventID.NO1:     // 인구 급증
                    resource.ApplyLeaderShip(-(int)Utils.GetPercentValue(resourceTable.leaderShipTable.Max, 0.2f));
                    resource.ApplyFood(-(int)(resourceTable.foodTable.Now * Mathf.Min((accumulateDate / 250f), 0.5f)));

                    ApplyEventUI(table);
                    break;
                case EventID.NO2:     // 식중독 발생
                    resource.ApplyLeaderShip(-(int)Utils.GetPercentValue(resourceTable.leaderShipTable.Max, 0.3f));
                    ApplyEventUI(table);
                    evt.SubscribeThreeDayEvent(() => { resource.ApplyPopulation(-(int)Utils.GetPercentValue(resourceTable.populationTable.Max, 0.1f)); });
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
            uint accumulateDate = evt.GetWeek.GetAccumulateDate;

            switch (_eventId)
            {
                case EventID.NO3:     // 단풍잎 이야기
                    resource.ApplyLeaderShip((int)resource.GetLeaderShipResource(0.1d));
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
                    resource.ApplyPopulation(-(int)(resource.GetResourceTable.populationTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.1f), 0.5f)));
                    evt.switchCondition.SwitchON(SwitchID.NO2);
                    ApplyEventUI(table);
                    break;
                case EventID.NO7:         // 톱니바퀴 개발
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    evt.switchCondition.SwitchON(SwitchID.NO1);
                    ApplyEventUI(table);
                    break;
                case EventID.NO8:         // 감독관 배치
                    resource.ApplyLeaderShip((int)Utils.GetPercentValue(resourceTable.leaderShipTable.Max, 0.1f));
                    evt.switchCondition.SwitchON(SwitchID.NO3);
                    ApplyEventUI(table);
                    break;
                case EventID.NO9:         // 장비를 정지합니다
                    GameEvent.Instance.WeekUploadTime = 2.0f;
                    ApplyEventUI(table);
                    break;
                case EventID.NO10:        // 부정 부패
                    resource.ApplyFood(-(int)resource.GetFoodResource(0.2d));
                    resource.ApplyFood(-(int)(resourceTable.foodTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.1f), 0.5f)));
                    resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(0.2d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO11:        // 노후 부품 발견
                    evt.switchCondition.SwitchOff(SwitchID.NO1);
                    ApplyEventUI(table);
                    break;
                case EventID.NO12:        // 식량 분쟁
                    resource.ApplyFood(-(int)resource.GetFoodResource(0.3d));
                    resource.ApplyFood(-(int)(resourceTable.foodTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.1f), 0.5f)));
                    ApplyEventUI(table);
                    break;
                case EventID.NO13:        // 식량 창고 개선
                    resource.ApplyMaxFood((int)evt.GetTrain.GetTrainAmount().Storage);
                    resource.ApplyPopulation(-(int)resource.GetFoodResource(0.2d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO14:        // 어이쿠 손이 미끄러졌네
                    resource.ApplyFood(-(int)resource.GetFoodResource(0.3d));
                    resource.ApplyFood(-(int)(resourceTable.foodTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.1f), 0.5f)));
                    resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(0.1d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO15:        // 재배 시설 강화
                    resource.ApplyFood((int)evt.GetTrain.GetTrainAmount().Cultivation);
                    resource.ApplyFood((int)resource.GetFoodResource(0.3d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO16:        //베이비 붐 시대
                    resource.ApplyPopulation((int)((double)evt.GetTrain.GetTrainAmount().GuestRoom * resource.GetFoodResource(0.1d)));
                    ApplyEventUI(table);
                    break;
                case EventID.NO17:        // 의문의 지도자
                    resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(0.1d));
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
            uint accumulateDate = evt.GetWeek.GetAccumulateDate;

            switch (_eventId)
            {
                case EventID.NO18:        // 식인 사건
                    resource.ApplyPopulation(-(int)resource.GetPopulationResource(0.3d));
                    resource.ApplyPopulation(-(int)(resourceTable.populationTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.1f), 0.5f)));
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(0.4d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO19:        // 인신 공양
                    resource.ApplyPopulation(-(int)resource.GetPopulationResource(0.5d));
                    resource.ApplyFood((int)resource.GetFoodResource(0.1d));
                    resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(-0.4d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO20:        // 반란 발생
                    if (SpecialBubbleSystem.Instance.SpawnSpecialBubble(SpecialBubbleType.REBELLION))
                    {
                        resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(-0.1d));
                        resource.ApplyPopulation(-(int)(resourceTable.populationTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.15f), 0.5f)));
                        ApplyEventUI(table);
                    }
                    table.IsEventOn = false;
                    break;
                case EventID.NO21:        // 집단 시위
                    if (SpecialBubbleSystem.Instance.SpawnSpecialBubble(SpecialBubbleType.DEMONSTRATE))
                    {
                        resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(-0.1d));
                        resource.ApplyFood(-(int)(resourceTable.foodTable.Now * Mathf.Min((float)Utils.GetPercentValue((accumulateDate / 250f), 0.15f), 0.5f)));
                        ApplyEventUI(table);
                    }
                    table.IsEventOn = false;
                    break;
                case EventID.NO22:        // 알 수 없는 종교
                    if (SpecialBubbleSystem.Instance.SpawnSpecialBubble(SpecialBubbleType.FALSE_RELIGION))
                    {
                        resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(0.1d));
                        ApplyEventUI(table);
                    }
                    table.IsEventOn = false;
                    break;
                case EventID.NO23:        // 만족하는 복지
                    resource.ApplyLeaderShip((int)resource.GetLeaderShipResource(0.3d));
                    ApplyEventUI(table);
                    break;
                case EventID.NO24:        // 만성 피로
                    table._nextEvent = false;
                    evt.SubscribeThreeDayEvent(() => { resource.ApplyLeaderShip(-(int)resource.GetLeaderShipResource(0.1d)); });
                    evt.SubscribeTwoDayEvent(() => { resource.ApplyPopulation(-(int)resource.GetPopulationResource(0.1d)); });
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