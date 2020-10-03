using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Bubble
{
    public class SpecialBubbleButton : MonoBehaviour
    {
        public SpecialBubbleType SpecialBubbleType = SpecialBubbleType.REBELLION;
        public void SpecialBubbleUP()
        {
            switch (SpecialBubbleType)
            {
                case SpecialBubbleType.REBELLION:
                    GameEvent.Instance.SubscribeFourDayEvent(() => { GameEvent.Instance.GetResource.ApplyPopulation(-1); });
                    break;
                case SpecialBubbleType.DEMONSTRATE:
                    InGame.Bubble.BubbleSystem.Instance.IsAbleProduce = false;
                    break;
                case SpecialBubbleType.FALSE_RELIGION:
                    GameEvent.Instance.SubscribeSixDayEvent(() => { GameEvent.Instance.GetResource.ApplyLeaderShip(-1); });
                    break;
                default:
                    break;
            }
        }
        public void _OnClickLeftRebellion()
        {
            GameEvent.Instance.GetResource.ApplyLeaderShip(-2);
        }
        public void _OnClickRightRebellion()
        {
            GameEvent.Instance.GetResource.ApplyFood(-2);
        }
        public void _OnClickDemonstrate()
        {
            GameEvent.Instance.GetResource.ApplyFood(-2);
            InGame.Bubble.BubbleSystem.Instance.IsAbleProduce = true;
        }

        public void _OnClickFalseReligion()
        {
            SpecialBubbleButton[] specialBubbleButtons = FindObjectsOfType<SpecialBubbleButton>();
            int count = 0;
            foreach (var item in specialBubbleButtons)
            {
                if (item.SpecialBubbleType == SpecialBubbleType.FALSE_RELIGION) ++count;
            }
            GameEvent.Instance.GetResource.ApplyPopulation(count
                * System.Convert.ToInt32((float)GameEvent.Instance
                .GetResource
                .GetResourceTable
                .populationTable.Max * 0.1f));
        }



    }
}