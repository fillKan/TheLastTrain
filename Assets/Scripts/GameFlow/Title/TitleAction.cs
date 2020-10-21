using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAction : MonoBehaviour
{
    [SerializeField]
    private int MainGameScenceIndex;

    [Space()][SerializeField]
    private GameObject[] ThemaObjets;
    
    public void MainGameLoad() {
        SceneManager.LoadScene(MainGameScenceIndex);
    }

    private void Awake()
    {
        ThemaObjets[Random.Range(0, ThemaObjets.Length)].SetActive(true);
        DontDestroyOnLoad(gameObject);
    }
}
