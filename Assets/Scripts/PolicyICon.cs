using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyICon : MonoBehaviour
{
    public Animator GetAnimator;
    public Image GetImage;

    private void Reset()
    {
        Debug.Assert(TryGetComponent(out GetImage));
        Debug.Assert(TryGetComponent(out GetAnimator));
    }
}
