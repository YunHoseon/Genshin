using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance = null;
    public string nextSceneName;

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
    //레이싱 게임
    /* public int nSceneID = -1;
     GameObject[] cars = new GameObject[3];
     GameObject playerCar;
     public RecordInfo[] infos = new RecordInfo[4];

     void OnGUI()
     {
         if (nSceneID == 0)
         {
             if (GUI.Button(new Rect(400, 200, 100, 30), "Start"))
                 SceneManager.LoadScene("Basic02");
         }
         else if (nSceneID == 1)
         {
             int carCnt = 0;
             GameObject[] gameObjects = SceneManager.GetSceneByName("Basic02").GetRootGameObjects();

             for (int i = 0; i < gameObjects.Length; i++)
             {
                 if (gameObjects[i].CompareTag("OtherCar"))
                 {
                     cars[carCnt] = gameObjects[i];
                     carCnt++;
                 }
             }
             playerCar = GameObject.FindGameObjectWithTag("Player");

             if (!(playerCar.GetComponent<MoveMyCar>().lap >= 3))   //플레이어 완주x
                 return;

             for (int i = 0; i < cars.Length; i++)
                 if (!cars[i].GetComponent<AutoDriving>().moveStop)   //하나라도 멈추지 않음
                     return;

             SaveRecord();
             SceneManager.LoadScene("EndScene");
         }
         else if (nSceneID == 2)
         {
             if (GUI.Button(new Rect(400, 300, 100, 30), "Restart"))
                 SceneManager.LoadScene("StartScene");
         }
     }

     void SaveRecord()
     {
         for (int i = 0; i < 3; i++)
         {
             infos[i].time = cars[i].GetComponent<AutoDriving>().recordTime;
             infos[i].name = cars[i].name;
         }
         infos[cars.Length].time = playerCar.GetComponent<MoveMyCar>().recordTime;
         infos[cars.Length].name = playerCar.name;

         Debug.Log("myCar의 기록 : " + infos[cars.Length].time);
     }*/

    //플래피 버드
    /*public int nSceneID = -1;
    GameObject player = null;
    public float playerScore = 0;

    void OnGUI()
    {
        if (nSceneID == 0)
        {
            if (GUI.Button(new Rect(350, 300, 100, 30), "Start"))
                SceneManager.LoadScene("Basic03");
        }
        else if(nSceneID == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player.GetComponent<Bird>().gameOver)
            {
                playerScore = player.GetComponent<Bird>().score;
                SceneManager.LoadScene("EndScene");
            }
        }
        else if(nSceneID == 2)
        {
            if (GUI.Button(new Rect(350, 300, 100, 30), "Restart"))
                SceneManager.LoadScene("StartScene");
            GUI.Button(new Rect(350, 350, 100, 30), "Quit");
        }
    }*/

    //2D슈팅
    /*public int nSceneID = -1;
    public float playerHP = 10;
    public string playerID = null;
    public float score = 0;

    void OnGUI()
    {
        if (nSceneID == 0)
        {
            playerHP = 10;
            score = 0;

            if (GUI.Button(new Rect(350, 300, 100, 30), "Start"))
            {
               SceneManager.LoadScene("2DProj");
            }
        }
        else if (nSceneID == 1)
        {
            Text text_ID = GameObject.Find("IDText").GetComponent<Text>();
            text_ID.text = playerID;

            if (playerHP <= 0)
                SceneManager.LoadScene("EndScene2D");
        }
        else if (nSceneID == 2)
        {
            Text text_Score = GameObject.Find("score").GetComponent<Text>();
            text_Score.text = playerID + "'S SCORE";
            if (GUI.Button(new Rect(350, 300, 100, 30), "Restart"))
                SceneManager.LoadScene("StartScene2D");
            GUI.Button(new Rect(350, 350, 100, 30), "Quit");
        }
    }*/

    //스파르탄
    /*private int nSceneID = -1;
    private float score = 0;
    private int count = 0;

    public int GetSceneID() { return nSceneID; }
    public void SetSceneID(int _id) { nSceneID = _id; }

    public float GetScore() { return score; }
    public void SetScore(float _score) { score = _score; }

    public int GetCount() { return count; }
    public void SetCount(int _cnt) { count = _cnt; }

    void OnGUI()
    {
        if (nSceneID == 0)
        {
            score = 0;
            count = 0;

            if (GUI.Button(new Rect(350, 300, 100, 30), "Start"))
            {
                SceneManager.LoadScene("Untitled");
            }
        }
        else if (nSceneID == 1)
        {
            if(count >= 5)
                SceneManager.LoadScene("EndSceneSpartan");
        }
        else if (nSceneID == 2)
        {
            if (GUI.Button(new Rect(350, 300, 100, 30), "Restart"))
                SceneManager.LoadScene("StartSceneSpartan");
        }
    }*/

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
