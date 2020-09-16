using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PolicyAniState
{
    OnPolicy,DisPolicy,LeftMove
}

public class PolicyICon : MonoBehaviour
{
    public Animator GetAnimator;
    public Image GetImage;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out GetImage));
        Debug.Assert(TryGetComponent(out GetAnimator));
    }
    public PolicyAniState GetAniState()
    {
        return (PolicyAniState)GetAnimator.GetInteger("AniStste");
    }
    public void SetAniStste(PolicyAniState aniState)
    {
        GetAnimator.SetInteger("AniStste", (int)aniState);
    }
}
