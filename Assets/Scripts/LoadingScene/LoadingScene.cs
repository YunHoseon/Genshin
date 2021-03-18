using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    AsyncOperation async;
    public Slider progressBar;

    private float randomTime = 0;

    void Start()
    {
        GameManager.Instance.SetSceneID(1);
        //StartCoroutine(LoadingNextScene(2));
    }

    void Update()
    {
        if(progressBar.value >= 1.0f)
        {
            SceneManager.LoadScene(2);
        }
        randomTime += Time.deltaTime;

        if (randomTime >= Random.Range(3.0f, 10.0f))
        {
            progressBar.value += 0.01f;
        }
        //DelayTime();
    }

    /*float delayTime = 0.0f;
    void DelayTime()
    {
        progressBar.value = async.progress;
        if (async.progress >= 0.9f)
        {
            delayTime += Time.deltaTime;
            if (delayTime > 2.0f)
            {
                async.allowSceneActivation = true;
            }
        }
    }

    IEnumerator LoadingNextScene(int sceneId)
    {
        async = SceneManager.LoadSceneAsync(sceneId);
        async.allowSceneActivation = false;     //씬 비활성화

        while (async.progress < 0.9f)
        {
            //do something
            yield return true;
        }
    }*/
}
