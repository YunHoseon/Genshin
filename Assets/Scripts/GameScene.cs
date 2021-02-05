using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SetSceneID(2);
    }
}
