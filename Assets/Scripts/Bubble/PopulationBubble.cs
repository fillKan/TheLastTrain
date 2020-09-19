using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Bubble
{
    public class PopulationBubble : Bubble, IBubble
    {
        public void _BubbleClicked()
        {
            IncreaseResourceInBubble(Vehicles.GUESTROOM);
        }
    }
}