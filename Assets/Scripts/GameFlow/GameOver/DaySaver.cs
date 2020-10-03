using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame.UI.Week;

public class DaySaver : MonoBehaviour
{
    private int mSumDay;
    private WeekTable mWeekTable;

    public void DaySave(int sumDay, WeekTable week)
    {
        mSumDay = sumDay; mWeekTable = week;
    }
    public string SumDay() => mSumDay.ToString();
    public string WeekDay() => $"{mWeekTable.years}/{mWeekTable.month}/{mWeekTable.day}";
}
