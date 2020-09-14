using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSystem : MonoSingleton<ChoiceSystem>
{
    private void Awake()
    {
        GameEvent.Instance.DescribeMonthEvent(ShowUpChoiceCards);
    }
    private void ShowUpChoiceCards()
    {
        // To do . . .
    }
}
