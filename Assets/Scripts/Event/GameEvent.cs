using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using InGame.UI.Week;
using InGame.UI.Resource;

/*
 * 
 * Usage
 * 
 *  //Set Month Event 
 *  GameEvent.Instance.SetMonthEvent(() => {});
 *  GameEvent.Instance.SetMonthEvent(() => { Debug.Log("매 달마다 실행되는 이벤트"); });
 *  GameEvent.Instance.SetMonthEvent(action);
 *  
 *  //Set Day Event
 *  GameEvent.Instance.SetDayEvent(() => {});
 *  GameEvent.Instance.SetDayEvent(() => { Debug.Log("10일, 20일 마다 실행되는 이벤트"); });
 *  GameEvent.Instance.SetDayEvent(action);
 *  
 *  //Set Bubble Event
 *  GameEvent.Instance.SetBubbleEvent(() => {});
 *  GameEvent.Instance.SetBubbleEvent(() => { Debug.Log("버블 생성 이벤트"); });
 *  GameEvent.Instance.SetBubbleEvent(action);
 *  
 *  //Get Resource Table
 *  GameEvent.Instance.GetResource.something();
 *  
 *  //Get Week Table
 *  GameEvent.Instance.GetWeek.something();
 *  
 *  // Renew Counting Speed
 *  WeekUploadTime : float
 *  
 *  // Initalization Week
 *  GameEvent.Instance.InitWeekTable
 *  
 *  //Initalization Resource
 *  GameEvent.Instance.InitResourceTable
 */

interface Iinit
{
    void Initialize();
}
[System.Serializable]
public struct WeekTable
{
    public uint years;
    public uint month;
    public uint day;
}

[System.Serializable]
public struct Table
{
    [Tooltip("현재 자원 수")]
    public uint Now;
    [Tooltip("최대 자원 수")]
    public uint Max;
}

[System.Serializable]
public class ResourceTable
{
    public Table populationTable;
    public Table foodTable;
    public Table leaderShipTable;
}

[System.Serializable]
public struct GazeTable
{
    public UnityEngine.UI.Image GazeImage;
    public UnityEngine.UI.Text GazeText;
}

public class GameEvent : MonoSingleton<GameEvent>
{
    private Resource _resource;
    private Week _week;

    public Resource GetResource
    {
        get
        {
            return _resource;
        }
    }
    public Week GetWeek
    {
        get
        {
            return _week;
        }
    }

    // Week Upload Time
    [Range(0.1f, 1.0f)] 
    [SerializeField] float WeekUploadTime = 1.0f;
    public float GetWeekUploadTime() => WeekUploadTime;

    // Initialization
    public WeekTable InitWeekTable;
    public ResourceTable InitResourceTable;

    // UI
    public UnityEngine.UI.Text WeekText;
    public UnityEngine.UI.Text AccumulateText;
    [Space]
    public GazeTable PopulationUITable;
    public GazeTable FoodUITable;
    public GazeTable LeaderShipUITable;

    

    public void SubscribeMonthEvent(Action action) => GetWeek.OnMonthEvent += action;
    public void DescribeMonthEvent(Action action) => GetWeek.OnMonthEvent -= action;
    public void SetMonthEvent(Action action) => GetWeek.OnMonthEvent = action;

    public void SubscribeDayEvent(Action action) => GetWeek.OnDayEvent += action;
    public void DescribeDayEvent(Action action) => GetWeek.OnDayEvent -= action;
    public void SetDayEvent(Action action) => GetWeek.OnDayEvent = action;

    public void SetSixDayEvent(Action action) => GetWeek.OnSixDayEvent = action;

    public void SubscribeBubbleEvent(Action action) => GetWeek.OnBubbleEvent += action;
    public void DescribeBubbleEvent(Action action) => GetWeek.OnBubbleEvent -= action;
    public void SetBubbleEvent(Action action) => GetWeek.OnBubbleEvent = action;

    void Awake()
    {
        _resource = new Resource(this);
        _week = new Week(this);
    }
    void OnEnable()
    {
        if (_resource != null && _week != null)
        {
            GetWeek.Initialize();
            GetResource.Initialize();
        }
        else throw new NullReferenceException($"_resource, _week NullRerenceException");
            
        StartCoroutine(GetWeek.EWeekProcess());
        StartCoroutine(GetResource.EResourceProcess());
    }
    void OnDisable()
    {
        StopCoroutine(GetWeek.EWeekProcess());
        StopCoroutine(GetResource.EResourceProcess());
    }
}
