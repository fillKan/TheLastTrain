using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Bubble
{
    public class FoodBubble : Bubble, IBubble
    {
        public void _BubbleClicked()
        {
            IncreaseResourceInBubble(Vehicles.CULTIVATION);
        }
    }

}