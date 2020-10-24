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

    [SerializeField]
    private UnityEngine.UI.Image TitleImage;
    [SerializeField]
    private UnityEngine.UI.Image Tap2PlayImage;
    
    [SerializeField]
    private Animator Tap2PlayAnim;

    private AsyncOperation mAsyncSceneLoad;

    public void MainGameLoad()
    {
        StartCoroutine(ESceneLoad());
    }

    private void Awake()
    {        
        Transform thema = ThemaParent.GetChild(Random.Range(0, ThemaParent.childCount));

                          thema.parent = null;
                          thema.gameObject.SetActive(true);
        DontDestroyOnLoad(thema.gameObject);
    }

    private IEnumerator ESceneLoad()
    {
        var async = 
            SceneManager.LoadSceneAsync(MainGameIndex, LoadSceneMode.Single);
            
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }
        Tap2PlayAnim.enabled = false;

        while (TitleImage.color.a > 0)
        {
            yield return null;

            float deltaTime = Time.deltaTime;

               TitleImage.color -= Color.black * deltaTime * 0.3f;
            Tap2PlayImage.color -= Color.black * deltaTime * 0.3f;
        }
        async.allowSceneActivation = true;
    }
}
