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
    [SerializeField] private int          AddResourceAmount;
    [SerializeField] private GameObject   AddCompartment;

    [Header("Policy Enforcement")]
    [SerializeField] private UnityEvent EnforcePolicy;

    public void ChooseThis()
    {
        // To do ...
    }
}