using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance = null;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //======================================================================

    private int sceneID = -1;

    private bool isStart = false;
    private bool isExitGame = false;

    public int GetSceneID() { return sceneID; }
    public void SetSceneID(int _id) { sceneID = _id; }

    public bool GetIsStart() { return isStart; }
    public void SetIsStart(bool _isStart) { isStart = _isStart; }

    public bool GetIsExitGame() { return isExitGame; }
    public void SetIsExitGame(bool _isExitGame) { isExitGame = _isExitGame; }

    void OnGUI()
    {
        if (sceneID == 0)      //start scene
        {
            if(isStart)
            {
                SceneManager.LoadScene("LoadingScene");
                //SceneManager.LoadScene("GameScene");
                isStart = false;
            }
        }
        else if (sceneID == 1) //loading scene
        {
            
        }
        else if (sceneID == 2) //game scene
        {
            if(isExitGame)
            {
                SceneManager.LoadScene("StartScene");
                isExitGame = false;
            }
        }
    }
}
