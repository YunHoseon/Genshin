using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager sInstance = null;
    public string nextSceneName;

    public static UIManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_UIManager");
                sInstance = newGameObject.AddComponent<UIManager>();
            }
            return sInstance;
        }
    }

    private float elementalSkillCooltime;
    private float elementalBurstCooltime;

    public float elementalSkillCooltime_ { get; set; }
    public float elementalBurstCooltime_ { get; set; }

    public void StartGame()
    {
        GameManager.Instance.SetIsStart(true);
    }

    public void GoToStartScene()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetIsExitGame(true);
    }
}
