using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Event
{
    public enum SwitchID
    {
        /// <summary>
        /// 톱니바퀴 (SWITCH)
        /// </summary>
        NO1,
        /// <summary>
        /// 사고 (SWITCH)
        /// </summary>
        NO2,
        /// <summary>
        /// 감독관 (SWITCH)
        /// </summary>
        NO3,
        /// <summary>
        /// 의문의 지도자 (SWITCH)
        /// </summary>
        NO4,
        /// <summary>
        /// 포교 (SWITCH)
        /// </summary>
        NO5,
        /// <summary>
        /// 식량 절약 (SWITCH)
        /// </summary>
        NO6,
        /// <summary>
        /// 인구 조절 (SWITCH)
        /// </summary>
        NO7,
        /// <summary>
        /// 의료 사업 (SWITCH)
        /// </summary>
        NO8,
        /// <summary>
        /// 야간 노동 (SWITCH)
        /// </summary>
        NO9,
        None,
    }

    public class SwitchCondition : MonoBehaviour
    {
        public List<SwitchTable> switchTable;

        private void Start()
        {
            switchTable.ForEach(table => {
                table.IsOn = false;
            });
        }

        /// <summary>
        /// 이벤트 스위치 ON : VOID
        /// </summary>
        /// <param name="switchID">작동시킬 스위치 이벤트</param>
        public void SwitchON(SwitchID switchID)
        {
            switchTable[(int)switchID].IsOn = true;
        }

        /// <summary>
        /// 이벤트 스위치 OFF : VOID
        /// </summary>
        /// <param name="switchID">작동시킬 스위치 이벤트</param>
        public void SwitchOff(SwitchID switchID)
        {
            switchTable[(int)switchID].IsOn = false;
        }

        /// <summary>
        /// 이벤트 스위치의 작동 여부 : BOOL
        /// </summary>
        /// <param name="switchID">작동시킬 스위치 이벤트</param>
        public bool IsSwitchState(SwitchID switchID)
        {
            return switchTable[(int)switchID].IsOn;
        }

        /// <summary>
        /// 모든 이벤트 스위치에 대한 정보 출력
        /// </summary>
        public void PrintAllSwitchState()
        {
            for (int i = 0; i < switchTable.Count; i++)
            {
                print($"{switchTable[i].name} : {switchTable[i].ID} : {switchTable[i].IsOn}");
            }
        }

        /// <summary>
        /// 이벤트 스위치 목록 검사 & 체크
        /// </summary>
        public void SwitchCheck()
        {
            EventEffect eventEffect = GameEvent.Instance.eventEffect;
            switchTable.ForEach(table => {
                switch (table.ID)
                {
                    case SwitchID.NO1:
                        if (eventEffect.eventTable[(int)EventID.NO7].IsEventOn == true)
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO2:
                        if (eventEffect.eventTable[(int)EventID.NO6].IsEventOn == true)
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO3:
                        if (eventEffect.eventTable[(int)EventID.NO8].IsEventOn == true)
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO4:
                        if (eventEffect.eventTable[(int)EventID.NO17].IsEventOn == true)
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO5:
                        
                        if (PolicySystem.Instance.IsExistAccumulatePolicy(Policy.MissionaryWork))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO6:
                        if (PolicySystem.Instance.IsExistAccumulatePolicy(Policy.FoodSaving))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO7:
                        if (PolicySystem.Instance.IsExistAccumulatePolicy(Policy.PopulationDownSize))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO8:
                        if (PolicySystem.Instance.IsExistAccumulatePolicy(Policy.MedicalIndustry))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO9:
                        if (PolicySystem.Instance.IsExistAccumulatePolicy(Policy.ExtraWork))
                        {
                            table.IsOn = true;
                        }
                        break;
                    default:
                        break;
                }
            });
        }
    }
}