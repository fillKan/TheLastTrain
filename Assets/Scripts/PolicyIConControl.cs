using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyIConControl : MonoBehaviour
{
    private const int ICON_COUNT = 2; 

    [SerializeField] private Animator[] mPoliyAnimator = new Animator[ICON_COUNT];
    [SerializeField] private    Image[]    mPoliyImage = new    Image[ICON_COUNT];

    private void Reset()
    {
        for (int i = 0; i < ICON_COUNT; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image)) {
                mPoliyImage[i] = image;
            }
            if (transform.GetChild(i).TryGetComponent(out Animator animator)) {
                mPoliyAnimator[i] = animator;
            }
        }
    }

    public void AddPolicy(Policy policy)
    {
        // To do ...
    }
}
