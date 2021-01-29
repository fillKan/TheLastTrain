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
    private Vector3 endPos;
    void OnEnable()
    {
        text = GetComponent<Text>();
        text.color = mColor;
        //startPos = transform.localPosition;
        //endPos = new Vector3(startPos.x, startPos.y + 500, startPos.z);
        StartCoroutine(Processing());
    }

    IEnumerator Processing()
    {
        while (gameObject.activeSelf)
        {
            if (text.color.a <= 0.1f)
                Destroy(this.transform.parent.gameObject);

            //transform.localPosition = Vector2.MoveTowards(startPos, endPos, DeltaTime * 5);
            //float y = Mathf.Lerp(transform.position.y, startPos.y + 20, DeltaTime);

            //transform.position = new Vector3(startPos.x, y, startPos.z);
            //text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.01f);
            text.color = Color.Lerp(text.color, new Color(1, 1, 1, 0), DeltaTime * 2);
            yield return null;
        }
    }
}
