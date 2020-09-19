using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InGame.UI.Week;

public class GameObserver : MonoSingleton<GameObserver>
{
    public int EndingIndex;

    public int SumDay;

    public WeekTable WeekTable;

    [SerializeField]
    private GameObject[] Themas = new GameObject[3];

    public void InfoReset()
    {
        SumDay = 0;
    }
    private void Awake() => Themas[Random.Range(0, 3)].SetActive(true);

    private void Start()
    {
        GameEvent.Instance.SubscribeBubbleEvent(() => SumDay++);
    }
    private void Update()
    {
        WeekTable = GameEvent.Instance.GetWeek.GetWeekTable;

        if (GameEvent.Instance.GetResource.GetResourceTable.foodTable.Now <= 0 ||
            GameEvent.Instance.GetResource.GetResourceTable.leaderShipTable.Now <= 0 ||
            GameEvent.Instance.GetResource.GetResourceTable.populationTable.Now <= 0)
        {
            GameObject day = new GameObject("DaySaver", typeof(DaySaver));

            day.GetComponent<DaySaver>().DaySave(SumDay, WeekTable);

            DontDestroyOnLoad(day);

            SceneManager.LoadScene(EndingIndex);
        }
    }
}
