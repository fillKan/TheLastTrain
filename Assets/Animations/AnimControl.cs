using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public void Disable()
    {
        if (gameObject.TryGetComponent(out Animator animator)) {
            if (animator.GetFloat("PlaySpeed") == -1)
            {
                
                gameObject.SetActive(false);
            }
        }
    }
}
