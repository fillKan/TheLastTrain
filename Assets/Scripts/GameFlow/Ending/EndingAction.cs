using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InGame.UI.Week;

public class EndingAction : MonoBehaviour
{
    [SerializeField] private Text SumDayText;
    [SerializeField] private Text EndDayText;

    private Transform mTitleAction;

    public void LoadScene(int sceneIndex)
    {
        Destroy(mTitleAction.gameObject);

        SceneManager.LoadScene(sceneIndex);
    }

    private void Start()
    {
        DaySaver day = 
             FindObjectOfType(typeof(DaySaver)) as DaySaver;

        mTitleAction =
            (FindObjectOfType(typeof(TitleAction)) as TitleAction).transform;

        for (int i = 0; i< mTitleAction.childCount; ++i) 
        {
            if (mTitleAction.GetChild(i).gameObject.activeSelf) {
                mTitleAction.GetChild(i).parent = transform;
            }
        }
        SumDayText.text = day.SumDay();
        EndDayText.text = day.WeekDay();

        Destroy(day.gameObject);
    }
}
