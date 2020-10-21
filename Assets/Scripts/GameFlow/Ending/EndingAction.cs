using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InGame.UI.Week;

public class EndingAction : MonoBehaviour
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
        Destroy((FindObjectOfType(typeof(TitleAction)) as TitleAction).gameObject);

        SceneManager.LoadScene(TitleSeceneIndex);
    }
    public void LoadMainGame()
    {
        Destroy((FindObjectOfType(typeof(TitleAction)) as TitleAction).gameObject);

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
