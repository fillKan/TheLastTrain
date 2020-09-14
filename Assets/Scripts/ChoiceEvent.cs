using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using InGame.UI.Resource;

public enum ResourceType
{
    None, Population, Food, Support
}

public class ChoiceEvent : MonoBehaviour
{
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
            Resource resource = GameEvent.Instance.GetResource;

            switch (AddResourceType)
            {
                case ResourceType.Population:
                    resource.ApplyPopulation(AddResourceAmount);
                    break;

                case ResourceType.Food:
                    resource.ApplyFood(AddResourceAmount);
                    break;

                case ResourceType.Support:
                    resource.ApplySupportResource(AddResourceAmount);
                    break;
            }
            // add compartment . . .
        }
    }
}