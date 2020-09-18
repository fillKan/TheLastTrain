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
        public void SwitchCheck()
        {
            switchTable.ForEach(table => {
                switch (table.ID)
                {
                    case SwitchID.NO1:
                        break;
                    case SwitchID.NO2:
                        break;
                    case SwitchID.NO3:
                        break;
                    case SwitchID.NO4:
                        break;
                    case SwitchID.NO5:
                        break;
                    case SwitchID.NO6:
                        break;
                    case SwitchID.NO7:
                        break;
                    case SwitchID.NO8:
                        break;
                    case SwitchID.NO9:
                        break;
                    default:
                        break;
                }
            });
        }
    }
}