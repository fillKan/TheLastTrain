using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PolicyAniState
{
    OnPolicy,DisPolicy,LeftMove
}

public class PolicyIcon : MonoBehaviour
{
    [SerializeField] private    Image mImage;
    [SerializeField] private Animator mAnimator;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out mImage));
        Debug.Assert(TryGetComponent(out mAnimator));
    }
    public void SetSprite(Sprite sprite) {
        mImage.sprite = sprite;
    }
    public void SetAniStste(PolicyAniState aniState) {
        mAnimator.SetInteger("AniStste", (int)aniState);
    }
}
