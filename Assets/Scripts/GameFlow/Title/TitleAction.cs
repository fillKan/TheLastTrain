using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAction : MonoBehaviour
{
    [SerializeField]
    private int MainGameIndex;

    [Space()]
    [SerializeField]
    private Transform ThemaParent;

    private AsyncOperation mAsnycSceneLoad;

    public void MainGameLoad()
    {
        if (mAsnycSceneLoad?.progress >= 0.9f) 
        {
            mAsnycSceneLoad.allowSceneActivation = true;
        }
    }

    private void Awake()
    {        
        Transform thema = ThemaParent.GetChild(Random.Range(0, ThemaParent.childCount));

                          thema.parent = null;
                          thema.gameObject.SetActive(true);
        DontDestroyOnLoad(thema.gameObject);
    }

    private void Start() {
        StartCoroutine(ESceneLoad());
    }

    private IEnumerator ESceneLoad()
    {
        yield return new WaitForSeconds(0.1f);

        mAsnycSceneLoad =
           SceneManager.LoadSceneAsync(MainGameIndex, LoadSceneMode.Single);

        mAsnycSceneLoad.allowSceneActivation = false;
    }
}
