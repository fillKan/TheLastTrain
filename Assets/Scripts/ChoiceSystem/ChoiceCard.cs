using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using InGame.UI.Resource;

public enum ResourceType
{
    None, Population, Food, Support
}

public class ChoiceCard : MonoBehaviour
{
    private Resource mTrainResource;

    [Range(0f, 1f)]
    [SerializeField] private float Probability;
    public float GetProbability => Probability;
    
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

                case ResourceType.Support:
                    mTrainResource.ApplySupportResource(AddResourceAmount);
                    break;
            }
            // add compartment . . .
        }

        if (IsEnforcementPolicy)
        {
            EnforcePolicy.Invoke();
        }
    }
}