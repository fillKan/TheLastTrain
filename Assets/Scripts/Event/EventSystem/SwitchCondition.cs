using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Event
{
    public enum SwitchID
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

        public void SwitchON(SwitchID switchID)
        {
            switchTable[(int)switchID].IsOn = true;
        }
        public void SwitchOff(SwitchID switchID)
        {
            switchTable[(int)switchID].IsOn = false;
        }
        public bool IsSwitchState(SwitchID switchID)
        {
            return switchTable[(int)switchID].IsOn;
        }
        public void PrintAllSwitchState()
        {
            for (int i = 0; i < switchTable.Count; i++)
            {
                print($"{switchTable[i].name} : {switchTable[i].ID} : {switchTable[i].IsOn}");
            }
        }
        public void SwitchCheck()
        {
            EventEffect eventEffect = GameEvent.Instance.eventEffect;
            IEnforcementable enforcementable;
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
                        
                        if (PolicySystem.Instance.GetPolicy.TryGetValue(Policy.MissionaryWork, out enforcementable))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO6:
                        if (PolicySystem.Instance.GetPolicy.TryGetValue(Policy.FoodSaving, out enforcementable))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO7:
                        if (PolicySystem.Instance.GetPolicy.TryGetValue(Policy.PopulationDownSize, out enforcementable))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO8:
                        if (PolicySystem.Instance.GetPolicy.TryGetValue(Policy.MedicalIndustry, out enforcementable))
                        {
                            table.IsOn = true;
                        }
                        break;
                    case SwitchID.NO9:
                        if (PolicySystem.Instance.GetPolicy.TryGetValue(Policy.ExtraWork, out enforcementable))
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