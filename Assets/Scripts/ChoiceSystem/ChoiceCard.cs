using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using InGame.UI.Resource;

public enum ResourceType
{
    None, Population, Food, LeaderShip
}

public enum Policy
{
    /// <summary>
    /// 포교
    /// </summary>
    MissionaryWork,
    /// <summary>
    /// 식량 절약
    /// </summary>
    FoodSaving,
    /// <summary>
    /// 인구 감축
    /// </summary>
    PopulationDownSize,
    /// <summary>
    /// 의료 산업
    /// </summary>
    MedicalIndustry,
    /// <summary>
    /// 야간 노동
    /// </summary>
    ExtraWork
}

public class ChoiceCard : MonoBehaviour
{
    private Resource mTrainResource;

    [Range(0f, 1f)]
    [SerializeField] private float[] Probabilities;
    public float[] GetProbabilities => Probabilities;

    [Space]
    [SerializeField] private bool IsAdditionCompartment;
    [SerializeField] private bool IsEnforcementPolicy;

    [Header("Add Compartment")]
    [SerializeField] private ResourceType AddResourceType;
    [SerializeField] private uint         AddResourceAmount;
    [SerializeField] private GameObject   AddCompartment;

    [Header("Policy Enforcement")]
    [SerializeField] private UnityEvent EnforcePolicy;

    public void ChooseThis()
    {
        if (IsAdditionCompartment)
        {
            if (mTrainResource == null) {
                mTrainResource = GameEvent.Instance.GetResource;
            }
            switch (AddResourceType)
            {
                case ResourceType.Population:
                    mTrainResource.ApplyPopulation(AddResourceAmount);
                    break;

                case ResourceType.Food:
                    mTrainResource.ApplyFood(AddResourceAmount);
                    break;

                case ResourceType.LeaderShip:
                    mTrainResource.ApplyLeaderShip(AddResourceAmount);
                    break;
            }
            // add compartment . . .
        }
        if (IsEnforcementPolicy)
        {
            EnforcePolicy.Invoke();
        }
        ChoiceSystem.Instance.NotifyChooseOne();
    }
}