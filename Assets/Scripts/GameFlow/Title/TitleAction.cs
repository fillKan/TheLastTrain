using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAction : MonoBehaviour
{
    [SerializeField]
    private int MainGameScenceIndex;
    public void MainGameLoad() => SceneManager.LoadScene(MainGameScenceIndex);
}
