using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyIConControl : MonoBehaviour
{
    private const int ICON_COUNT = 2;

    public class Preset
    {
        public Animator Animator;
        public Image Image;

        public Preset(Animator animator, Image image)
        {
            Animator = animator; Image = image;
        }
    }
    [SerializeField] private Animator[] PolicyAnimator = new Animator[ICON_COUNT];
    [SerializeField] private    Image[]    PolicyImage = new    Image[ICON_COUNT];

    private List<Preset> mLPolicy = new List<Preset>(ICON_COUNT);


    private void Reset()
    {
        for (int i = 0; i < ICON_COUNT; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image)) {
                PolicyImage[i] = image;
            }
            if (transform.GetChild(i).TryGetComponent(out Animator animator)) {
                PolicyAnimator[i] = animator;
            }
        }
    }
    private void OnEnable()
    {
        for (int i = 0; i < ICON_COUNT; i++)
        {
            mLPolicy.Add(new Preset(PolicyAnimator[i], PolicyImage[i]));
        }
    }
    public void AddPolicy(Policy policy)
    {
        // To do ...
    }
}
