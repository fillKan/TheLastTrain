using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        GameEvent.Instance.DescribeMonthEvent(ShowUpChoiceCards);
    }
    private void ShowUpChoiceCards()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Random.value <= TrainCardProbability)
            {
                EnableCard(i, TrainCards);
            }
            else
            {
                EnableCard(i, PolicyCards);
            }
        }
    }
    private void EnableCard(int index, ChoiceCard[] cards)
    {
        int selectIndex = 0;

        float probability = Random.value;

        float closestValue = 0f;

        for (int i = 0; i < cards.Length; i++)
        {
            if (Mathf.Abs(cards[i].GetProbability - probability) < closestValue) {
                closestValue = Mathf.Abs(cards[selectIndex = i].GetProbability - probability);
            }
        }
        cards[selectIndex].transform.localPosition = CadrPositions[index];

        cards[selectIndex].gameObject.SetActive(true);
    }
}
