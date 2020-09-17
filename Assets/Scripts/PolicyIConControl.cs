using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyIConControl : MonoBehaviour
{
    private const int ICON_COUNT = 3;

    [SerializeField] private PolicyICon[] policyICons = new PolicyICon[ICON_COUNT];

    private int MIndex = 1;
    private int RIndex
    { get => (MIndex + 1) > 2 ? 0 : (MIndex + 1); }
    private int LIndex
    { get => (MIndex - 1) < 0 ? 2 : (MIndex - 1); }

    private void Reset()
    {
        for (int i = 0; i < ICON_COUNT; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out PolicyICon policyICon)) {
                policyICons[i] = policyICon;
            }
        }
    }
    #region UI animation test
    //private void Update()
    //{
    //    if (Input.anyKeyDown)
    //    {
    //        AddPolicy((Policy)Random.Range(1, 6));
    //    }
    //}
    #endregion
    public void AddPolicy(Policy policy)
    {
        policyICons[RIndex].SetAniStste(PolicyAniState.LeftMove);
        policyICons[LIndex].SetAniStste(PolicyAniState.OnPolicy);
        policyICons[MIndex].SetAniStste(PolicyAniState.DisPolicy);
        policyICons[LIndex].GetImage.sprite = PolicyHub.Instance.GetPolicyICon(policy);

        MIndex = (MIndex + 1) > 2 ? 0 : (MIndex + 1);
    }
}
