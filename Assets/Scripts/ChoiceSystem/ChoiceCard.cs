using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using InGame.UI.Resource;

public enum ResourceType
{
    None, Population, Food, LeaderShip
}
public enum WhatToAdd
{
    Max, Now
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
    [SerializeField] private int          AddResourceAmount;
    [SerializeField] private WhatToAdd    AddResourceWhatTo;
    [SerializeField] private GameObject   AddCompartment;

    [Header("Policy Enforcement")]
    [SerializeField] private Policy EnforcePolicy;

    public void ChooseThis()
    {
        if (IsAdditionCompartment)
        {
            if (mTrainResource == null) {
                mTrainResource = GameEvent.Instance.GetResource;
            }
            switch (AddResourceWhatTo)
            {
                case WhatToAdd.Max:
                    switch (AddResourceType)
                    {
                        case ResourceType.Population:
                            GameEvent.Instance.InitResourceTable.populationTable.Max = 
                            (uint)Mathf.Max(0, GameEvent.Instance.InitResourceTable.populationTable.Max + AddResourceAmount);
                            break;

                        case ResourceType.Food:
                            GameEvent.Instance.InitResourceTable.foodTable.Max =
                            (uint)Mathf.Max(0, GameEvent.Instance.InitResourceTable.foodTable.Max + AddResourceAmount);
                            break;

                        case ResourceType.LeaderShip:
                            GameEvent.Instance.InitResourceTable.leaderShipTable.Max =
                            (uint)Mathf.Max(0, GameEvent.Instance.InitResourceTable.leaderShipTable.Max + AddResourceAmount);
                            break;
                    }
                    break;

                case WhatToAdd.Now:
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
                    break;
            }
            // add compartment . . .
        }
        if (IsEnforcementPolicy)
        {
            PolicyHub.Instance.Enforce(EnforcePolicy);
        }
        ChoiceSystem.Instance.NotifyChooseOne();
    }
}