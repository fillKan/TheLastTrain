using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] sprites;

    public int CurSequence = 0;

    private void Start()
    {
        GameEvent.Pause();
    }

    [ContextMenu("RIGHT")]
    public void OnRight()
    {
        if (sprites.Length == CurSequence + 1)
            Skip();
        if (sprites.Length <= CurSequence + 1)
            return;
        CurSequence += 1;
        targetImage.sprite = sprites[CurSequence];
    }
    [ContextMenu("LEFT")]
    public void OnLeft()
    {
        if (0 > CurSequence - 1)
            return;
        CurSequence -= 1;
        targetImage.sprite = sprites[CurSequence];
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        GameEvent.Pause();
    }
}
