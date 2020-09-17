using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyIConControl : MonoBehaviour
{
    private const int ICON_COUNT = 3;

    [SerializeField] private PolicyICon[] policyICons = new PolicyICon[ICON_COUNT];

    private int mEmptyIndex = 0;
    private int mRightIndex = 2;
    private int  mLeftIndex = 1;

    private void Reset()
    {
        for (int i = 0; i < ICON_COUNT; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out PolicyICon policyICon)) {
                policyICons[i] = policyICon;
            }
        }
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            AddPolicy((Policy)Random.Range(1, 6));
        }
    }
    public void AddPolicy(Policy policy)
    {
        policyICons[mRightIndex].SetAniStste(PolicyAniState.LeftMove);
        policyICons[mEmptyIndex].SetAniStste(PolicyAniState.OnPolicy);
        policyICons[ mLeftIndex].SetAniStste(PolicyAniState.DisPolicy);

        policyICons[mEmptyIndex].GetImage.sprite = PolicyHub.Instance.GetPolicyICon(policy);

        mRightIndex = (mRightIndex + 1) > 2 ? 0 : (mRightIndex + 1);
        mEmptyIndex = (mEmptyIndex + 1) > 2 ? 0 : (mEmptyIndex + 1);
         mLeftIndex = ( mLeftIndex + 1) > 2 ? 0 : ( mLeftIndex + 1);
    }
}
