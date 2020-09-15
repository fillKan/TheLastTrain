using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoiceSystem : MonoSingleton<ChoiceSystem>
{
    [SerializeField] [Range(0f, 1f)]
    private float TrainCardProbability;

    [SerializeField][Space]
    private Vector2[] CadrPositions;

    [SerializeField]
    private ChoiceCard[] TrainCards;

    [SerializeField]
    private ChoiceCard[] PolicyCards;

    private WeekTable mStartByWeekTable;

    private void Start()
    {
        GameEvent.Instance.SubscribeMonthEvent(ShowUpChoiceCards);

        mStartByWeekTable = GameEvent.Instance.GetWeek.GetWeekTable;

        ChoiceCard[] tempArray = new ChoiceCard[TrainCards.Length];

        MergeSort(TrainCards, 0, TrainCards.Length - 1, tempArray, (A, B) => A.GetProbabilities[0] < B.GetProbabilities[0]);
    }
    private void ShowUpChoiceCards()
    {
        for (int i = 0; i < 3; i++)
        {
            if (UnityEngine.Random.value <= TrainCardProbability && TrainCards.Length > 0)
            {
                EnableCard(i, TrainCards);
            }
            else if (PolicyCards.Length > 0)
            {
                EnableCard(i, PolicyCards);
            }
        }
    }
    private void EnableCard(int index, ChoiceCard[] cards)
    {
        int selectIndex = 0;

        float probability = UnityEngine.Random.value;

        float closestValue = float.MaxValue;

        int dayCondition = 0;

        if (GameEvent.Instance.GetWeek.GetWeekTable.years - mStartByWeekTable.years >= 0)
        { dayCondition = 2; }
        else 
        if (GameEvent.Instance.GetWeek.GetWeekTable.month - mStartByWeekTable.month >= 6)
        { dayCondition = 1; }

        for (int i = 0; i < cards.Length; i++)
        {
            float close = probability - cards[i].GetProbabilities[dayCondition];

            if (close < closestValue) {
                selectIndex = i;

                closestValue = close;
            }
        }
        cards[selectIndex].transform.localPosition = CadrPositions[index];

        cards[selectIndex].gameObject.SetActive(true);
    }

    void MergeSort<T>(T []sortingArray, int lowIndex, int highIndex, T []tempArray, Func<T,T,bool> isRValueBigger)
    {
        // 1. base condition
        if (lowIndex >= highIndex) return;

        // 2. divide
        int mid = (lowIndex + highIndex) / 2;

        // 3. conquer
        MergeSort(sortingArray, lowIndex,       mid, tempArray, isRValueBigger);
        MergeSort(sortingArray, mid + 1 , highIndex, tempArray, isRValueBigger);

        // 4. combine
        int i = lowIndex;
        int j = mid + 1;
        int k = lowIndex;

        for (; k <= highIndex; ++k)
        {
            if (j > highIndex)
            {
                tempArray[k] = sortingArray[i++];
            }
            else if (i > mid)
            {
                tempArray[k] = sortingArray[j++];
            }
            else if (isRValueBigger(sortingArray[i], sortingArray[j]))
            {
                tempArray[k] = sortingArray[i++];
            }
            else
            {
                tempArray[k] = sortingArray[j++];
            }
        }
        // 5. copy
        for (i = lowIndex; i <= highIndex; ++i)
        {
            sortingArray[i] = tempArray[i];
        }
    }
}
