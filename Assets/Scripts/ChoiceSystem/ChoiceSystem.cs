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

    private int mDayCondition;

    private void Start()
    {
        GameEvent.Instance.SubscribeMonthEvent(ShowUpChoiceCards);

        mStartByWeekTable = GameEvent.Instance.GetWeek.GetWeekTable;

        SortCardArray(TrainCards, mDayCondition = 0);
    }
    private void ShowUpChoiceCards()
    {
        if (UnityEngine.Random.value <= TrainCardProbability && TrainCards.Length > 0) {
            EnableCard(TrainCards);
        }
        else if (PolicyCards.Length > 0) {
            EnableCard(PolicyCards);
        }
    }
    private void EnableCard(ChoiceCard[] cards)
    {
        if (GameEvent.Instance.GetWeek.GetWeekTable.years - mStartByWeekTable.years > 0)
        {
            if (mDayCondition != 2) {
                SortCardArray(TrainCards, mDayCondition = 2);
            }
        }
        else if (GameEvent.Instance.GetWeek.GetWeekTable.month - mStartByWeekTable.month >= 6)
        {
            if (mDayCondition != 1) {
                SortCardArray(TrainCards, mDayCondition = 1);
            }
        }
        float probability = UnityEngine.Random.value;

        int[] selectIndexes = new int[3] { int.MaxValue, int.MaxValue, int.MaxValue };

        for (int i = 0; i < 3; i++)
        {
            float closestValue = float.MaxValue;

            for (int j = 0; j < cards.Length; j++)
            {
                if (j == selectIndexes[Mathf.Max(0, i - 2)] || 
                    j == selectIndexes[Mathf.Max(0, i - 1)] || 
                    j == selectIndexes[0]) continue;

                float close = probability - cards[j].GetProbabilities[mDayCondition];

                if (close < closestValue)
                {
                    selectIndexes[i] = j;

                    closestValue = close;
                }
            }
            cards[selectIndexes[i]].transform.localPosition = CadrPositions[i];

            cards[selectIndexes[i]].gameObject.SetActive(true);
        }
    }

    private void SortCardArray(ChoiceCard[] sortingArray, int sortingIndex)
    {
        Debug.Log($"Sorting : {sortingIndex}");

        ChoiceCard[] tempArray = new ChoiceCard[sortingArray.Length];

        MergeSort(sortingArray, 0, sortingArray.Length - 1, tempArray,
            (A, B) => A.GetProbabilities[sortingIndex] < B.GetProbabilities[sortingIndex]);
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
