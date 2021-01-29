using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image targetImage;
    public Image skipImage;
    public Sprite[] sprites;

    public int CurSequence = 0;

    private void OnEnable()
    {
        CurSequence = 0;
        targetImage.sprite = sprites[CurSequence];
    }

    public bool IsEndPage => sprites.Length == CurSequence + 1;

    [ContextMenu("NEXT")]
    public void OnNext()
    {
        if (IsEndPage)
            ClosePanel();
        if (sprites.Length <= CurSequence + 1)
            return;
        CurSequence += 1;
        if (IsEndPage)
            skipImage.gameObject.SetActive(false);
        targetImage.sprite = sprites[CurSequence];
    }
    public void OnSkip() => ClosePanel();
    public void ClosePanel() => gameObject.SetActive(false);
}
