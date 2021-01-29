using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Color32 mColor;
    public Text popupText { get; set; }

    private float DeltaTime => Time.deltaTime * Time.timeScale;

    private Vector3 startPos;
    private Vector3 endPos;

    private ObjectPool objectPool;
    public void SetPool(ObjectPool objectPool) => this.objectPool = objectPool;

    void OnEnable()
    {
        popupText = GetComponent<Text>();
        popupText.color = mColor;
        //startPos = transform.localPosition;
        //endPos = new Vector3(startPos.x, startPos.y + 500, startPos.z);
        StartCoroutine(Processing());
    }
    private void OnDisable()
    {
        objectPool = null;
    }
    IEnumerator Processing()
    {
        while (gameObject.activeSelf)
        {
            if (popupText.color.a <= 0.1f)
                objectPool?.push(transform.parent.gameObject);

            popupText.color = Color.Lerp(popupText.color, new Color(1, 1, 1, 0), DeltaTime * 2);
            yield return null;
        }
    }
}
