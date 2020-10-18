using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] sprites;

    public int CurSequence = 0;

    private void OnEnable()
    {
        CurSequence = 0;
        targetImage.sprite = sprites[CurSequence];
    }

    [ContextMenu("NEXT")]
    public void OnNext()
    {
        if (sprites.Length == CurSequence + 1)
            ClosePanel();
        if (sprites.Length <= CurSequence + 1)
            return;
        CurSequence += 1;
        targetImage.sprite = sprites[CurSequence];
    }
    public void OnSkip() => ClosePanel();
    public void ClosePanel() => gameObject.SetActive(false);
}
