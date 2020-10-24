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

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void Start()
    {
        DaySaver day = 
             FindObjectOfType(typeof(DaySaver)) as DaySaver;

        GameObject.FindGameObjectWithTag("Thema").transform.parent = transform;

        SumDayText.text = day.SumDay();
        EndDayText.text = day.WeekDay();

        Destroy(day.gameObject);
    }
}
