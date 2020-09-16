using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public string PlaySpeed;

    private Animator mAnimator;

    private void Caching()
    {
        if (mAnimator == null) {
            Debug.Assert(TryGetComponent(out mAnimator));
        }
    }
    public void Disable()
    {
        Caching();

        if (mAnimator.GetFloat(PlaySpeed.GetHashCode()) < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
