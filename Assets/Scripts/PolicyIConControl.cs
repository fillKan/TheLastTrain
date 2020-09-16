using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyIConControl : MonoBehaviour
{
    private const int ICON_COUNT = 3;

    [SerializeField] private PolicyICon[] policyICons = new PolicyICon[ICON_COUNT];

    private void Reset()
    {
        for (int i = 0; i < ICON_COUNT; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out PolicyICon policyICon)) {
                policyICons[i] = policyICon;
            }
        }
    }
    public void AddPolicy(Policy policy)
    {
        // To do ...
    }
}
