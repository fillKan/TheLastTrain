using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Bubble
{
    public class LeaderShipBubble : Bubble, IBubble
    {
        public void _BubbleClicked()
        {
            IncreaseResourceInBubble(Vehicles.EDUCATION);
            //Debug.Log("Porridge Bubble");
        }
    }
}