using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InGame.UI.Week;

public class EndingObserver : MonoBehaviour
{
    [SerializeField]
    private int TitleSeceneIndex;
    [SerializeField]
    private int MainGameSeceneIndex;
    [SerializeField]
    private int EndingSeceneIndex;
    [SerializeField]
    private Text SumDayText;
    [SerializeField]
    private Text EndDayText;

    public void LoadTitle()
    {
        SceneManager.LoadScene(TitleSeceneIndex);
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene(MainGameSeceneIndex);
    }
    private void Start()
    {
        DaySaver day = FindObjectOfType(typeof(DaySaver)) as DaySaver;

        SumDayText.text = day.SumDay();
        EndDayText.text = day.WeekDay();

        Destroy(day.gameObject);
    }
}
