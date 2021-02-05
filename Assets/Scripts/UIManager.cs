using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.SetIsStart(true);
    }

    public void GoToStartScene()
    {
        GameManager.Instance.SetIsExitGame(true);
    }
}
