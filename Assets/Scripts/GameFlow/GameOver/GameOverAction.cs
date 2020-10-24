using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InGame.UI.Week;

public class GameOverAction : MonoBehaviour
{
    [SerializeField]
    private int mEndingIndex;

    private int mSurvivalDay;

    private Week mGameDateTime;

    private ResourceTable mResourceTable;

    private void Start()
    {
        GameEvent.Instance.SubscribeBubbleEvent(() => mSurvivalDay++);

        mGameDateTime =
            GameEvent.Instance.GetWeek;

        mResourceTable = 
            GameEvent.Instance.GetResource.GetResourceTable;
    }
    private void Update()
    {
        if (IsGameOver())
        {
            GameObject day = new GameObject("DaySaver", typeof(DaySaver));

            if (day.TryGetComponent(out DaySaver daySaver))
            {
                daySaver.DaySave(mSurvivalDay, mGameDateTime.GetWeekTable);

                DontDestroyOnLoad(day);
            }
            SceneManager.LoadScene(mEndingIndex);
        }
    }
    private bool IsGameOver()
    {
        return mResourceTable.foodTable.Now       <= 0 ||
               mResourceTable.leaderShipTable.Now <= 0 ||
               mResourceTable.populationTable.Now <= 0;
    }
}
