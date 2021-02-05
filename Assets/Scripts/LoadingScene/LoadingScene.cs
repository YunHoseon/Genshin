using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    AsyncOperation async;

    void Start()
    {
        GameManager.Instance.SetSceneID(1);
        StartCoroutine(LoadingNextScene(2));
    }

    void Update()
    {
        DelayTime();
    }

    float delayTime = 0.0f;
    void DelayTime()
    {
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
    }
}
