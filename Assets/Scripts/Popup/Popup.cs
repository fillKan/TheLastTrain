using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Color32 mColor;
    private Text text;

    private float DeltaTime => Time.deltaTime * Time.timeScale;

    private Vector3 startPos;
    void OnEnable()
    {
        text = GetComponent<Text>();
        text.color = mColor;
        startPos = transform.position;

        StartCoroutine(Processing());
    }

    IEnumerator Processing()
    {
        while (gameObject.activeSelf)
        {
            if (text.color.a <= 0)
                Destroy(this.transform.parent.gameObject);
            float y = Mathf.Lerp(transform.position.y, startPos.y + 20, DeltaTime);

            transform.position = new Vector3(startPos.x, y, startPos.z);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.01f);
            yield return null;
        }
    }
}
